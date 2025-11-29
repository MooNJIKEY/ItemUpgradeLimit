# Item Upgrade Mod for Valheim

This mod allows you to increase the maximum upgrade level for items in Valheim.

## Features

- Configurable maximum upgrade level for different item categories:
  - Weapons (swords, bows, battle axes, etc.)
  - Armor (helmets, chest pieces, leg armor, shoulder armor)
  - Shields
  - Tools (pickaxes, wood axes, etc.)
- Ability to set individual levels for specific items
- Fully configurable via config file

## Installation

1. Make sure you have BepInEx installed
2. Copy `ItemUpgradeMod.dll` to `BepInEx/plugins/ItemUpgradeMod/` folder
3. Run the game once to generate the config file
4. Edit the config file at `BepInEx/config/com.valheim.itemupgrademod.cfg`

## Configuration

After running the game with the mod for the first time, open the config file:
`BepInEx/config/com.valheim.itemupgrademod.cfg`

### Category-based Settings:

```ini
[Item Categories]
# Maximum level for weapons
WeaponMaxLevel = 10

# Maximum level for armor
ArmorMaxLevel = 10

# Maximum level for shields
ShieldMaxLevel = 10

# Maximum level for tools
ToolMaxLevel = 10

# Default maximum level for other items
DefaultMaxLevel = 8
```

### Individual Item Settings:

You can set specific levels for particular items:

```ini
[Custom Items]
# Format: ItemName:Level,ItemName2:Level2
# Example:
ItemLevels = SwordIron:15,HelmetBronze:12,ShieldWood:8
```

## Example Item Names

Here are some item names you can use:

**Weapons:**
- SwordIron - Iron Sword
- SwordBronze - Bronze Sword
- AxeBronze - Bronze Axe
- Bow - Bow
- BowFineWood - Finewood Bow

**Armor:**
- HelmetBronze - Bronze Helmet
- HelmetIron - Iron Helmet
- ArmorBronzeChest - Bronze Chest Armor
- ArmorIronChest - Iron Chest Armor
- ArmorBronzeLegs - Bronze Leg Armor

**Shields:**
- ShieldWood - Wood Shield
- ShieldBronzeBuckler - Bronze Buckler
- ShieldIronTower - Iron Tower Shield

**Tools:**
- PickaxeIron - Iron Pickaxe
- AxeIron - Iron Axe (for wood)

## Important!

- Level values must be between 1 and 20
- You will need appropriate materials to upgrade items to new levels
- The mod works on the client, but for multiplayer it's recommended to install it on the server

## Building

To compile the mod you need:
1. .NET Framework 4.8
2. Visual Studio 2019 or newer (or dotnet CLI)

Run the command:
```bash
dotnet build ItemUpgradeMod.csproj
```

The compiled file will be automatically copied to the plugins folder.

## Support

If you encounter issues:
1. Check the BepInEx log file: `BepInEx/LogOutput.log`
2. Make sure all dependencies are installed correctly
3. Verify that item names in the config are spelled correctly

Version: 1.0.0

