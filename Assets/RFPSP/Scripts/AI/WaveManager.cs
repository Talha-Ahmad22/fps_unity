//WaveManager.cs by Azuline Studios© All Rights Reserved
//Spawns NPCs from NPC Spawners for successive waves using several 
//parameters to control spawn timing and amounts.
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]

public class MultiDimensionalInt
{
	[Tooltip("Total number of NPCs to spawn for this wave.")] 
	public int[] NpcCounts;
	[Tooltip("Maximum number of NPCs from the spawner that can be active in the scene at once.")]
	public int[] NpcLoads;
	[Tooltip("Delay between spawning of NPCs for this wave.")]
	public float[] NpcDelay;
	[Tooltip("The NPC Prefabs that will be spawned for this wave.")]
	public GameObject[] NpcTypes;
}

public class WaveManager : MonoBehaviour {

	public static WaveManager instance;

    private void Awake()
    {
        instance = this;
    }


    private FPSPlayer FPSPlayerComponent;
	[Tooltip("The NPC Spawner objects that the Wave Manager will spawn NPC's from. The Waves list parameters correspond the order of these spawners from top to bottom.")]
	public List<NPCSpawner> NpcSpawners = new List<NPCSpawner>();
	[Tooltip("This list contains information for NPC wave spawning. The array sizes and order correspond with the Npc Spawners list. The Waves list can be expanded to add new waves of varying combinations of NPCs and parameters.")] 
	public MultiDimensionalInt[] waves;
	[Tooltip("Time before wave begins.")]
	public float warmupTime = 30.0f;
	private float startTime = -512;
	private float countDown;
	[HideInInspector]
	public int NpcsToSpawn;
	[HideInInspector]
	public int killedNpcs;
	[HideInInspector]
	public int waveNumber;

	[Tooltip("Sound FX played when wave starts.")]
	public AudioClip waveStartFx;
	[Tooltip("Sound FX played when wave ends.")]
	public AudioClip waveEndFx;
	private AudioSource asource;
	private bool fxPlayed;
	private bool fxPlayed2;
	private bool lastWave;

	[HideInInspector]
	public WaveText WaveText;
	[HideInInspector]
	public WaveText WaveTextShadow;
	[HideInInspector]
	public WarmupText WarmupText;
	[HideInInspector]
	public WarmupText WarmupTextShadow;

	private Color tempColor;
	private Color tempColor2;

	private Text WarmupUIText1;
	private Text WarmupUIText2;
	private Vector2 warmupTextPos1Orig;
	private Vector2 warmupTextPos2Orig;
	private Vector2 warmupTextPos1;
	private Vector2 warmupTextPos2;
	public TMP_Text waveNumberText;
	public TMP_Text killsNumberText;

	[Header ("Incoming Waver Timer")]
	public TMP_Text timeRemainingText;
	public GameObject newWaveStartedText;

	[HideInInspector] public bool newWaveBegun;

	Color defaultIncomingWaveTimerColor;
	Color incomingWaveTimerWhenLessThanThree = Color.red;
    
	[Header("Boss Settings")]
    public GameObject[] bossPrefabs; // assign in inspector
    public float bossHealthMultiplier = 20f;
    public float bossDamageMultiplier = 10f;


	private int totalKills;


    void Start () {
		FPSPlayerComponent =  Camera.main.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>();
		asource = gameObject.AddComponent<AudioSource>();
		asource.spatialBlend = 0.0f;

		WaveText = FPSPlayerComponent.waveUiObj.GetComponent<WaveText>();
		WaveTextShadow = FPSPlayerComponent.waveUiObjShadow.GetComponent<WaveText>();
		
		//initialize health amounts on GUIText objects
		WaveText.waveGui = waveNumber;
		WaveTextShadow.waveGui = waveNumber;	
		WaveText.waveGui2 = NpcsToSpawn - killedNpcs;
		WaveTextShadow.waveGui2 = NpcsToSpawn - killedNpcs;

		WarmupText = FPSPlayerComponent.warmupUiObj.GetComponent<WarmupText>();
		WarmupTextShadow = FPSPlayerComponent.warmupUiObjShadow.GetComponent<WarmupText>();

		tempColor = WarmupText.textColor;
		tempColor2 = WarmupTextShadow.textColor; 

		WarmupText.warmupGui = countDown;
		WarmupTextShadow.warmupGui = countDown;

		WarmupUIText1 = WarmupText.GetComponent<Text>();
		WarmupUIText2 = WarmupTextShadow.GetComponent<Text>();

		warmupTextPos1Orig = WarmupUIText1.rectTransform.anchoredPosition;
		warmupTextPos2Orig = WarmupUIText2.rectTransform.anchoredPosition;

        int totalWaves = 10;
        int spawnerCount = NpcSpawners.Count;
		//waves = new MultiDimensionalInt[totalWaves];

		//for (int w = 0; w < totalWaves; w++)
		//{
		//    MultiDimensionalInt wave = new MultiDimensionalInt();
		//    wave.NpcCounts = new int[spawnerCount];
		//    wave.NpcLoads = new int[spawnerCount];
		//    wave.NpcDelay = new float[spawnerCount];
		//    wave.NpcTypes = new GameObject[spawnerCount];

		//    for (int i = 0; i < spawnerCount; i++)
		//    {
		//        wave.NpcCounts[i] = 3 + w * 2;           // Increase enemy count per wave
		//        wave.NpcLoads[i] = Mathf.Clamp(1 + w, 1, 10); // Gradually increase active load
		//        wave.NpcDelay[i] = Mathf.Max(0.5f, 2.0f - (w * 0.1f)); // Faster spawn rate
		//        wave.NpcTypes[i] = NpcSpawners[i].NPCPrefab; // Set default NPC type
		//    }

		//    waves[w] = wave;
		//}


		defaultIncomingWaveTimerColor = timeRemainingText.color;

        StartCoroutine(StartWave());
	}

	void FixedUpdate(){	
		if(WaveText.waveGui2 != NpcsToSpawn - killedNpcs){
			WaveText.waveGui2 = NpcsToSpawn - killedNpcs;
			WaveTextShadow.waveGui2 = NpcsToSpawn - killedNpcs;
		}

		if (waveNumberText != null)
			waveNumberText.text = waveNumber.ToString();

		if (timeRemainingText != null && newWaveBegun)
		{
			if (countDown > 0.0f)
			{
				timeRemainingText.gameObject.SetActive(true);
				timeRemainingText.color = countDown > 3 ? defaultIncomingWaveTimerColor : incomingWaveTimerWhenLessThanThree;
				timeRemainingText.text = "NEXT WAVE BEGINS IN: " + Mathf.CeilToInt(countDown) + "s";
			}
			else
			{
				timeRemainingText.gameObject.SetActive(false);
				newWaveStartedText.gameObject.SetActive(true);
				newWaveBegun = false;
			}

		}

		
            
    }

	public IEnumerator StartWave(){
		newWaveBegun = true;
		countDown = warmupTime;
		WarmupText.warmupGui = countDown;
		WarmupTextShadow.warmupGui = countDown;	
		killedNpcs = 0;
		NpcsToSpawn = 0;
		//if(waveNumber <= waves.Length){
		//	if(waveNumber < waves.Length){
		//		waveNumber ++;
		//	}else{
		//		//start again from first wave if last wave was completed
		//		lastWave = true;
		//		waveNumber = 1;
		//	}
		//}else{
		//	waveNumber = 1;
		//}

		waveNumber++;
		WaveText.waveGui = waveNumber;
		WaveTextShadow.waveGui = waveNumber;	

		tempColor.a = 1.0f;
		tempColor2.a = 1.0f;

		WarmupText.waveBegins = false;
		WarmupTextShadow.waveBegins = false;

		if(waveNumber > 1 || lastWave){
            // Pause and open shop before starting next wave
            ShopManager sm = FindObjectOfType<ShopManager>();
			sm.OpenShop();
            startTime = Time.time;
			WarmupText.waveComplete = true;
			WarmupTextShadow.waveComplete = true;
			if(waveEndFx && !fxPlayed2){
				asource.PlayOneShot(waveEndFx, 1.0f);
				FPSPlayerComponent.StartCoroutine(FPSPlayerComponent.ActivateBulletTime(1.0f));
				fxPlayed2 = true;
			}
			if(lastWave){lastWave = false;}
		}


        //initialize NPC Spawner objects for spawning of this wave
        //      for (int i = 0; i < NpcSpawners.Count; i++){
        //	NpcSpawners[i].NPCPrefab = waves[waveNumber - 1].NpcTypes[i];
        //	NpcSpawners[i].NpcsToSpawn = waves[waveNumber - 1].NpcCounts[i];
        //	NpcSpawners[i].maxActiveNpcs = waves[waveNumber - 1].NpcLoads[i];
        //	NpcSpawners[i].spawnDelay = waves[waveNumber - 1].NpcDelay[i];
        //	NpcsToSpawn += NpcSpawners[i].NpcsToSpawn;
        //	NpcSpawners[i].pauseSpawning = true;
        //	NpcSpawners[i].spawnedNpcAmt = 0;
        //	NpcSpawners[i].huntPlayer = true;
        //	NpcSpawners[i].unlimitedSpawning = false;
        //}

        for (int i = 0; i < NpcSpawners.Count; i++)
        {
            if (waveNumber % 10 == 0)
            {
                // Boss Wave
                var bossPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Length)];
                NpcSpawners[i].NPCPrefab = bossPrefab;
                NpcSpawners[i].NpcsToSpawn = 1;
                NpcSpawners[i].maxActiveNpcs = 1;
                NpcSpawners[i].spawnDelay = 0f;

                // Scale boss manually after spawn
                StartCoroutine(ScaleBossNextSpawn(bossPrefab));
            }
            else
            {
                // Normal Wave
                NpcSpawners[i].NPCPrefab = NpcSpawners[i].NPCPrefab; // already assigned in Inspector
                NpcSpawners[i].NpcsToSpawn = 3 + waveNumber * 2;
                NpcSpawners[i].maxActiveNpcs = Mathf.Clamp(1 + waveNumber, 1, 15);
                NpcSpawners[i].spawnDelay = Mathf.Max(0.5f, 2.0f - (waveNumber * 0.05f));
            }

            NpcsToSpawn += NpcSpawners[i].NpcsToSpawn;
            NpcSpawners[i].pauseSpawning = true;
            NpcSpawners[i].spawnedNpcAmt = 0;
            NpcSpawners[i].huntPlayer = true;
            NpcSpawners[i].unlimitedSpawning = false;
        }


        //spawn wave
        while (true){

			WarmupUIText1.rectTransform.anchoredPosition = warmupTextPos1Orig;
			WarmupUIText2.rectTransform.anchoredPosition = warmupTextPos2Orig;
			warmupTextPos1 = warmupTextPos1Orig;
			warmupTextPos2 = warmupTextPos2Orig;

			if(startTime + 3.00 < Time.time){
				WarmupText.waveComplete = false;
				WarmupTextShadow.waveComplete = false;
				countDown -= Time.deltaTime;
				WarmupText.warmupGui = countDown;
				WarmupTextShadow.warmupGui = countDown;
			}

			WarmupUIText1.enabled = true;
			WarmupUIText2.enabled = true;
			WarmupUIText1.color = tempColor; 
			WarmupUIText2.color = tempColor2; 

			//start spawning NPCs for this wave
			if(countDown <= 0.0f){
				if(waveStartFx && !fxPlayed){
					for(int i = 0; i < NpcSpawners.Count; i++){
						NpcSpawners[i].pauseSpawning = false;
					}
	
					WarmupText.waveBegins = true;
					WarmupTextShadow.waveBegins = true;

					fxPlayed = true;
					fxPlayed2 = false;
					asource.PlayOneShot(waveStartFx, 1.0f);
				}
			}

			if(countDown <= -2.75f){
				StartCoroutine(FadeWarmupText());
				fxPlayed = false;
				yield break;
			}

			yield return null;

		}

	}

	IEnumerator FadeWarmupText(){

		while(true){

			tempColor.a -= Time.deltaTime;
			tempColor2.a -= Time.deltaTime;
			
			WarmupUIText1.color = tempColor; 
			WarmupUIText2.color = tempColor2; 

			warmupTextPos1.y -= Time.deltaTime * 9.0f;
			warmupTextPos2.y -= Time.deltaTime * 9.0f;

			WarmupUIText1.rectTransform.anchoredPosition = warmupTextPos1;
			WarmupUIText2.rectTransform.anchoredPosition = warmupTextPos2;
			
			if(tempColor.a <= 0.0f && tempColor2.a <= 0.0f){
				WarmupUIText1.enabled = false;
				WarmupUIText2.enabled = false;
				yield break;
			}

			yield return null;
			
		}
	}

    IEnumerator ScaleBossNextSpawn(GameObject bossPrefab)
    {
        yield return new WaitForSeconds(0.2f); // wait for NPC to be fully spawned

        // Find all spawned NPCs
        var allEnemies = FindObjectsOfType<CharacterDamage>();

        foreach (var cd in allEnemies)
        {
            // Make sure it's the boss prefab and hasn't already been scaled
            if (cd.name.Contains(bossPrefab.name) && cd.hitPoints < 9999f) // avoid scaling twice
            {
				Debug.Log(cd.hitPoints);
                // Scale health
                cd.hitPoints *= bossHealthMultiplier;

                // Scale attack (via NPCAttack)
                var npcAttack = cd.GetComponent<NPCAttack>();
                if (npcAttack != null)
                {
                    npcAttack.damage = Mathf.RoundToInt(npcAttack.damage * bossDamageMultiplier);
                    Debug.Log($"Boss {cd.name} scaled → HP: {cd.hitPoints}, Damage: {npcAttack.damage}");
                }

                cd.transform.localScale *= 10.0f;

                break; // done with first matching boss
            }
        }
    }

	public void AddKill(int amount)
	{
		totalKills += amount;
		killsNumberText.text = totalKills.ToString();

		LeanTween.cancel(killsNumberText.gameObject);
		LeanTween.scale(killsNumberText.gameObject, Vector2.one * 2f, 0.15f).setEaseInBounce().setOnComplete(() =>
		{
			LeanTween.scale(killsNumberText.gameObject, Vector2.one, 0.15f).setEaseOutBounce();
        });

    }


}
