using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ShopItemGenerator
{
    [MenuItem("Tools/Generate Shop Items")]
    public static void GenerateItems()
    {
        string folder = "Assets/ShopItems";
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets", "ShopItems");

        int created = 0;

        // COMMON (10)
        created += CreateItem("Protein Bar", "Small health boost", ItemRarity.Common, 5,
            new Effect(UpgradeType.MaxHealth, 10));
        created += CreateItem("Minor Damage Chip", "Slightly increases damage", ItemRarity.Common, 5,
            new Effect(UpgradeType.Damage, 0.1f));
        created += CreateItem("Loose Change", "Increases coin drop", ItemRarity.Common, 5,
            new Effect(UpgradeType.CoinBonus, 1));
        created += CreateItem("Light Vitamins", "Boosts health regen", ItemRarity.Common, 6,
            new Effect(UpgradeType.HealthRegen, 2));
        created += CreateItem("Lucky Charm", "Increases coin drop chance", ItemRarity.Common, 6,
            new Effect(UpgradeType.CoinChance, 0.05f));
        created += CreateItem("Reinforced Padding", "Adds small max HP", ItemRarity.Common, 6,
            new Effect(UpgradeType.MaxHealth, 15));
        created += CreateItem("Painkillers", "Boosts regen rate slightly", ItemRarity.Common, 6,
            new Effect(UpgradeType.HealthRegen, 3));
        created += CreateItem("Ammo Tracker", "Increases coin bonus", ItemRarity.Common, 5,
            new Effect(UpgradeType.CoinBonus, 2));
        created += CreateItem("Muscle Juice", "Small damage boost", ItemRarity.Common, 6,
            new Effect(UpgradeType.Damage, 0.15f));
        created += CreateItem("Cheap Scope", "Coin chance boost", ItemRarity.Common, 5,
            new Effect(UpgradeType.CoinChance, 0.07f));

        // UNCOMMON (5)
        created += CreateItem("Regen Stim", "Boosts regen and health", ItemRarity.Uncommon, 12,
            new Effect(UpgradeType.HealthRegen, 5), new Effect(UpgradeType.MaxHealth, 25));
        created += CreateItem("Enhanced Ammo", "Bonus coins + damage", ItemRarity.Uncommon, 14,
            new Effect(UpgradeType.CoinBonus, 2), new Effect(UpgradeType.Damage, 0.25f));
        created += CreateItem("Combat Vest", "HP and coin chance", ItemRarity.Uncommon, 13,
            new Effect(UpgradeType.MaxHealth, 30), new Effect(UpgradeType.CoinChance, 0.1f));
        created += CreateItem("Surgical Injection", "Massive regen", ItemRarity.Uncommon, 14,
            new Effect(UpgradeType.HealthRegen, 8));
        created += CreateItem("Stacked Wallet", "Big coin bonus", ItemRarity.Uncommon, 15,
            new Effect(UpgradeType.CoinBonus, 5));

        // RARE (3)
        created += CreateItem("Nanofiber Armor", "Huge HP & regen", ItemRarity.Rare, 25,
            new Effect(UpgradeType.MaxHealth, 50), new Effect(UpgradeType.HealthRegen, 10));
        created += CreateItem("Golden Coin Core", "High coin bonus and chance", ItemRarity.Rare, 28,
            new Effect(UpgradeType.CoinBonus, 6), new Effect(UpgradeType.CoinChance, 0.15f));
        created += CreateItem("Prototype Laser Core", "Massive damage boost", ItemRarity.Rare, 30,
            new Effect(UpgradeType.Damage, 0.5f));

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Generated {created} ShopItems in {folder}/");
    }

    static int CreateItem(string name, string desc, ItemRarity rarity, int cost, params Effect[] effects)
    {
        var item = ScriptableObject.CreateInstance<ShopItem>();
        item.itemName = name;
        item.description = desc;
        item.rarity = rarity;
        item.baseCost = cost;
        item.effects = new List<ItemEffect>();

        foreach (var effect in effects)
        {
            item.effects.Add(new ItemEffect
            {
                upgradeType = effect.type,
                value = effect.value
            });
        }

        string safeName = name.Replace(" ", "_");
        AssetDatabase.CreateAsset(item, $"Assets/ShopItems/{safeName}.asset");
        return 1;
    }

    struct Effect
    {
        public UpgradeType type;
        public float value;
        public Effect(UpgradeType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
