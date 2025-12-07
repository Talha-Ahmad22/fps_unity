using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject shopPanel;
    public TMP_Text coinText;
    public Button closeButton;

    [Header("Shop Slots (4 total)")]
    public List<Button> itemButtons; // Assign 4 buttons
    public List<TMP_Text> itemLabels; // Assign 4 labels (Name + Cost)

    [Header("All Possible Items")]
    public List<ShopItem> allItems;

    private ShopItem[] currentItems = new ShopItem[4];
    private bool[] itemPurchased = new bool[4];

    void Start()
    {
        closeButton.onClick.AddListener(CloseShop);
        shopPanel.SetActive(false);
        SetupButtonListeners();
    }

    void Update()
    {
        coinText.text = "COINS: " + GameManager.Instance.coins;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CloseShop();
        }
    }

    void SetupButtonListeners()
    {
        for (int i = 0; i < itemButtons.Count; i++)
        {
            int index = i;
            itemButtons[i].onClick.AddListener(() => TryPurchaseItem(index));
        }
    }

    public void OpenShop()
    {
        Time.timeScale = 0f;
        shopPanel.SetActive(true);
        GenerateRandomItems();
    }

    public void CloseShop()
    {
        Time.timeScale = 1f;
        shopPanel.SetActive(false);
    }

    void GenerateRandomItems()
    {
        for (int i = 0; i < 4; i++)
        {
            currentItems[i] = GetRandomItemByRarity();
            itemPurchased[i] = false;
            UpdateItemUI(i);
        }
    }

    void UpdateItemUI(int index)
    {
        var item = currentItems[index];
        itemLabels[index].text = $"{item.itemName} ({item.baseCost}c)";
        itemButtons[index].interactable = true;
    }

    void TryPurchaseItem(int index)
    {
        if (itemPurchased[index]) return;

        ShopItem item = currentItems[index];
        int cost = item.baseCost;

        if (GameManager.Instance.coins >= cost)
        {
            GameManager.Instance.coins -= cost;
            ApplyItemEffect(item);
            itemPurchased[index] = true;
            itemButtons[index].interactable = false;
        }
    }

    void ApplyItemEffect(ShopItem item)
    {
        foreach (var effect in item.effects)
        {
            switch (effect.upgradeType)
            {
                case UpgradeType.Damage:
                    PlayerStats.Instance.damageMultiplier += effect.value;
                    break;

                case UpgradeType.MaxHealth:
                    var fps = FindObjectOfType<FPSPlayer>();
                    if (fps != null)
                    {
                        fps.maximumHitPoints += (int)effect.value;
                        fps.hitPoints = fps.maximumHitPoints;
                        fps.healthText.text = fps.maximumHitPoints.ToString();
                        fps.healthBar.value = fps.maximumHitPoints;
                    }
                    break;

                case UpgradeType.HealthRegen:
                    PlayerStats.Instance.regenEnabled = true;
                    PlayerStats.Instance.regenRate += effect.value;
                    break;

                case UpgradeType.CoinBonus:
                    PlayerStats.Instance.bonusCoinAmount += (int)effect.value;
                    break;

                case UpgradeType.CoinChance:
                    PlayerStats.Instance.bonusCoinChance = Mathf.Clamp01(PlayerStats.Instance.bonusCoinChance + effect.value);
                    break;
            }
        }
    }


    ShopItem GetRandomItemByRarity()
    {
        float roll = Random.value;
        List<ShopItem> pool;

        if (roll < 0.6f)
            pool = allItems.FindAll(i => i.rarity == ItemRarity.Common);
        else if (roll < 0.9f)
            pool = allItems.FindAll(i => i.rarity == ItemRarity.Uncommon);
        else
            pool = allItems.FindAll(i => i.rarity == ItemRarity.Rare);

        return pool[Random.Range(0, pool.Count)];
    }
}
