using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float damageMultiplier = 1f;
    public float bonusCoinChance = 0.25f;
    public int bonusCoinAmount = 1;
    public bool regenEnabled = false;
    public float regenRate = 10f;
    public float regenDelay = 3f;

    void Awake()
    {
        Instance = this;
    }
}
