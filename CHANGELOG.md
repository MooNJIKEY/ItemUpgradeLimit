# Changelog

All notable changes to this project will be documented in this file.

## [1.0.2] - 2025-01-XX

### Fixed
- **КРИТИЧЕСКОЕ ИСПРАВЛЕНИЕ**: Исправлена проблема, когда мод становился бесполезным в ванильной игре из-за ограничения уровня верстака
- Добавлен патч для CraftingStation.GetLevel(), который обходит ограничение уровня верстака
- Теперь предметы можно улучшать до настроенного максимального уровня независимо от уровня верстака

### Technical
- Добавлен Harmony патч для CraftingStation.GetLevel()
- Верстак теперь возвращает уровень 20 для обхода ограничений
- Фактический лимит улучшения по-прежнему контролируется через m_maxQuality

## [1.0.1] - 2025-11-29

### Added
- Configurable maximum upgrade levels for items
- Category-based settings (weapons, armor, shields, tools)
- Custom item-specific level settings
- Full BepInEx configuration support

### Fixed
- Compatibility with Valheim 0.221.4
- Removed invalid Harmony patches
- Added proper ObjectDB patching

### Technical
- Uses Harmony patches for ObjectDB, InventoryGui
- Supports BepInEx 5.4.2202+
- Tested with Valheim 0.221.4
