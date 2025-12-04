# Item Upgrade Mod for Valheim

[Русская версия](README.md)

This mod allows you to configure the maximum upgrade level for items in Valheim.

## 🎯 Features

- **Category-based settings** - weapons, armor, shields, tools
- **Individual settings** - custom level for each item
- **Simple configuration** - via config file
- **Workbench level bypass** - upgrade items regardless of workbench level
- **Full compatibility** with Valheim 0.221.4+

## 📦 Installation

### Via mod manager (recommended)

1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or another mod manager
2. Find `ItemUpgradeMod` in the mod list
3. Click "Install"
4. Launch the game through the mod manager

### Manual installation

1. Install [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Download the latest release of the mod
3. Extract to `BepInEx/plugins/ItemUpgradeMod/`
4. Launch the game

## ⚙️ Configuration

After the first launch, open the file:
```
BepInEx/config/com.valheim.itemupgrademod.cfg
```

### Example configuration

```ini
[Item Categories]
WeaponMaxLevel = 10      # Weapons
ArmorMaxLevel = 10       # Armor
ShieldMaxLevel = 10      # Shields
ToolMaxLevel = 10        # Tools
DefaultMaxLevel = 8      # Other items

[Custom Items]
# Format: ItemName:Level
ItemLevels = SwordIron:15,HelmetBronze:12
```

## 🎮 Usage

1. Configure levels in the config
2. Restart the game
3. Create/upgrade items
4. Enjoy the new levels!

**Important**: The mod automatically bypasses the workbench level restriction, so you can upgrade items to the configured maximum level regardless of your workbench level.

## 📋 Item Names

See [example_config.cfg](ItemUpgradeMod/example_config.cfg) for the full list.

## 🐛 Issues?

[Create an Issue](https://github.com/MooNJIKEY/ItemUpgradeLimit/issues) on GitHub.

## 📜 License

MIT License

## 🙏 Credits

- BepInEx Team
- Valheim Community
