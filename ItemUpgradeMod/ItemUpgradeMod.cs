using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemUpgradeMod
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class ItemUpgradeMod : BaseUnityPlugin
    {
        public const string PluginGUID = "com.valheim.itemupgrademod";
        public const string PluginName = "ItemUpgradeMod";
        public const string PluginVersion = "1.0.2";

        private readonly Harmony harmony = new Harmony(PluginGUID);

        // Singleton instance
        public static ItemUpgradeMod Instance;

        // Configuration entries
        public static ConfigEntry<bool> ModEnabled;
        public static ConfigEntry<int> WeaponMaxLevel;
        public static ConfigEntry<int> ArmorMaxLevel;
        public static ConfigEntry<int> ShieldMaxLevel;
        public static ConfigEntry<int> ToolMaxLevel;
        public static ConfigEntry<int> DefaultMaxLevel;
        public static ConfigEntry<string> CustomItemLevels;

        // Dictionary for custom item levels
        public static Dictionary<string, int> CustomLevels = new Dictionary<string, int>();

        private void Awake()
        {
            Instance = this;
            
            // General settings
            ModEnabled = Config.Bind("General", "Enabled", true,
                "Enable or disable the mod");

            // Category-based settings
            WeaponMaxLevel = Config.Bind("Item Categories", "WeaponMaxLevel", 10,
                new ConfigDescription("Maximum upgrade level for weapons", new AcceptableValueRange<int>(1, 20)));

            ArmorMaxLevel = Config.Bind("Item Categories", "ArmorMaxLevel", 10,
                new ConfigDescription("Maximum upgrade level for armor pieces", new AcceptableValueRange<int>(1, 20)));

            ShieldMaxLevel = Config.Bind("Item Categories", "ShieldMaxLevel", 10,
                new ConfigDescription("Maximum upgrade level for shields", new AcceptableValueRange<int>(1, 20)));

            ToolMaxLevel = Config.Bind("Item Categories", "ToolMaxLevel", 10,
                new ConfigDescription("Maximum upgrade level for tools (pickaxe, axe, etc.)", new AcceptableValueRange<int>(1, 20)));

            DefaultMaxLevel = Config.Bind("Item Categories", "DefaultMaxLevel", 8,
                new ConfigDescription("Default maximum upgrade level for items not in other categories", new AcceptableValueRange<int>(1, 20)));

            // Custom item-specific settings
            CustomItemLevels = Config.Bind("Custom Items", "ItemLevels", "",
                new ConfigDescription("Comma-separated list of item:level pairs. Example: SwordIron:15,HelmetBronze:12,ShieldWood:8"));

            // Parse custom item levels
            ParseCustomItemLevels();

            if (ModEnabled.Value)
            {
                harmony.PatchAll();
                Logger.LogInfo($"{PluginName} v{PluginVersion} loaded successfully!");
            }
            else
            {
                Logger.LogInfo($"{PluginName} is disabled in config.");
            }
        }

        private void ParseCustomItemLevels()
        {
            CustomLevels.Clear();
            if (string.IsNullOrWhiteSpace(CustomItemLevels.Value))
                return;

            string[] pairs = CustomItemLevels.Value.Split(',');
            foreach (string pair in pairs)
            {
                string[] parts = pair.Trim().Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int level))
                {
                    CustomLevels[parts[0].Trim()] = level;
                    Logger.LogInfo($"Custom level set for {parts[0].Trim()}: {level}");
                }
            }
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }

        public static int GetMaxLevelForItem(ItemDrop.ItemData item)
        {
            if (!ModEnabled.Value)
                return item.m_shared.m_maxQuality;

            string itemName = item.m_shared.m_name;

            // Check if there's a custom level for this specific item
            if (CustomLevels.TryGetValue(itemName, out int customLevel))
                return customLevel;

            // Check item category
            if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeaponLeft ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                return WeaponMaxLevel.Value;
            }

            if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs ||
                item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder)
            {
                return ArmorMaxLevel.Value;
            }

            if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
            {
                return ShieldMaxLevel.Value;
            }

            if (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool)
            {
                return ToolMaxLevel.Value;
            }

            return DefaultMaxLevel.Value;
        }

        // Get the maximum level from all configured settings
        // This is used to set workbench level to allow all configured upgrades
        public static int GetMaxConfiguredLevel()
        {
            if (!ModEnabled.Value)
                return 5; // Default workbench max level

            int maxLevel = Math.Max(WeaponMaxLevel.Value, 
                Math.Max(ArmorMaxLevel.Value, 
                Math.Max(ShieldMaxLevel.Value, 
                Math.Max(ToolMaxLevel.Value, DefaultMaxLevel.Value))));

            // Check custom levels too
            foreach (var customLevel in CustomLevels.Values)
            {
                if (customLevel > maxLevel)
                    maxLevel = customLevel;
            }

            return maxLevel;
        }
    }

    // Patch ObjectDB to modify max quality when items are loaded
    [HarmonyPatch(typeof(ObjectDB), "Awake")]
    public static class ObjectDB_Awake_Patch
    {
        static void Postfix(ObjectDB __instance)
        {
            if (!ItemUpgradeMod.ModEnabled.Value || __instance == null)
                return;

            // Modify all items in the database
            foreach (var item in __instance.m_items)
            {
                if (item == null) continue;
                
                var itemDrop = item.GetComponent<ItemDrop>();
                if (itemDrop?.m_itemData != null)
                {
                    int maxLevel = ItemUpgradeMod.GetMaxLevelForItem(itemDrop.m_itemData);
                    itemDrop.m_itemData.m_shared.m_maxQuality = maxLevel;
                }
            }
            
            Debug.Log("ItemUpgradeMod: Item max qualities updated!");
        }
    }

    // Patch to ensure workbench shows correct upgrade levels
    [HarmonyPatch(typeof(InventoryGui), "SetupUpgradeItem")]
    public static class InventoryGui_SetupUpgradeItem_Patch
    {
        static void Prefix(InventoryGui __instance, ItemDrop.ItemData item)
        {
            if (!ItemUpgradeMod.ModEnabled.Value || item == null)
                return;

            // Update the max quality before UI setup
            int maxLevel = ItemUpgradeMod.GetMaxLevelForItem(item);
            item.m_shared.m_maxQuality = maxLevel;
        }
    }

    // Patch InventoryGui.DoCrafting to ensure proper upgrade handling
    [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
    public static class InventoryGui_DoCrafting_Patch
    {
        static void Prefix(InventoryGui __instance, Player player)
        {
            if (!ItemUpgradeMod.ModEnabled.Value || player == null)
                return;

            // Ensure all inventory items have correct max quality
            if (player.GetInventory() != null)
            {
                foreach (var item in player.GetInventory().GetAllItems())
                {
                    if (item != null)
                    {
                        int maxLevel = ItemUpgradeMod.GetMaxLevelForItem(item);
                        item.m_shared.m_maxQuality = maxLevel;
                    }
                }
            }
        }
    }

    // Patch CraftingStation.GetLevel to bypass workbench level restriction
    // In vanilla, workbench max level is 5, so it won't allow upgrades above level 5
    // This patch ensures workbench level is at least as high as the maximum configured upgrade level
    [HarmonyPatch(typeof(CraftingStation), "GetLevel")]
    public static class CraftingStation_GetLevel_Patch
    {
        static void Postfix(CraftingStation __instance, ref int __result)
        {
            if (!ItemUpgradeMod.ModEnabled.Value || __instance == null)
                return;

            // Check if this is a workbench
            string stationName = __instance.m_name?.ToLower() ?? "";
            string stationPrefabName = __instance.name?.ToLower() ?? "";
            
            if (stationName.Contains("workbench") || stationPrefabName.Contains("workbench") || 
                stationName == "$piece_workbench" || __instance.m_workbenchExtension)
            {
                // Get the maximum configured level from mod settings
                int maxConfiguredLevel = ItemUpgradeMod.GetMaxConfiguredLevel();
                
                // Return the maximum between actual workbench level and configured max level
                // This ensures workbench can handle upgrades up to configured level
                // In vanilla, workbench max is 5, but if mod sets items to level 10,
                // workbench will return level 10 to allow those upgrades
                __result = Math.Max(__result, maxConfiguredLevel);
            }
        }
    }
}

