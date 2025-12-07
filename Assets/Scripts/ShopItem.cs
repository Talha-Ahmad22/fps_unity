using UnityEngine;
using System.Collections.Generic;

public enum ItemRarity { Common, Uncommon, Rare }
public enum UpgradeType { Damage, MaxHealth, HealthRegen, CoinBonus, CoinChance }

[System.Serializable]
public class ItemEffect
{
    public UpgradeType upgradeType;
    public float value;
}

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    [TextArea] public string description;
    public ItemRarity rarity;
    public int baseCost;

    public List<ItemEffect> effects = new List<ItemEffect>();
}
