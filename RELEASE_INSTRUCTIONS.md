# Инструкция по подготовке релиза для Thunderstore

## Шаги для создания релиза v1.0.2

### 1. Сборка мода

Для сборки DLL файла выполните:

```bash
cd ItemUpgradeMod
dotnet build --configuration Release
```

**Требования:**
- .NET SDK 6.0 или новее
- `assembly_valheim.dll` должен быть доступен (обычно находится в `valheim_Data/Managed/assembly_valheim.dll`)

После успешной сборки DLL будет автоматически скопирован в:
- `BepInExPack_Valheim/BepInEx/plugins/ItemUpgradeMod/` (для тестирования)
- `ItemUpgradeMod/ThunderstorePackage/plugins/ItemUpgradeMod/` (для упаковки)

### 2. Упаковка для Thunderstore

После сборки выполните:

```bash
package_mod.bat
```

Или вручную:

```powershell
cd ItemUpgradeMod\ThunderstorePackage
Compress-Archive -Path * -DestinationPath ..\..\ItemUpgradeMod_v1.0.2.zip -Force
```

### 3. Структура архива для Thunderstore

Архив должен содержать:
```
ItemUpgradeMod_v1.0.2.zip
├── manifest.json
├── README.md
└── plugins/
    └── ItemUpgradeMod/
        └── ItemUpgradeMod.dll
```

### 4. Загрузка на Thunderstore

1. Перейдите на https://valheim.thunderstore.io/
2. Войдите в аккаунт
3. Перейдите в раздел "Upload"
4. Загрузите файл `ItemUpgradeMod_v1.0.2.zip`
5. Проверьте информацию о моде:
   - Name: `MooNJIKEY-ItemUpgradeLimit`
   - Version: `1.0.2`
   - Description: "Allows you to configure the maximum upgrade level for items in Valheim. Bypasses workbench level restrictions."
   - Dependencies: `denikson-BepInExPack_Valheim-5.4.2202`

### 5. Изменения в версии 1.0.2

- **КРИТИЧЕСКОЕ ИСПРАВЛЕНИЕ**: Исправлена проблема с ограничением уровня верстака
- Верстак теперь автоматически адаптируется к максимальному настроенному уровню
- Добавлен патч для `CraftingStation.GetLevel()` для обхода ограничений ванильной игры
- Верстак возвращает максимум между реальным уровнем и настроенным максимальным уровнем

### 6. Проверка перед загрузкой

Убедитесь что:
- [ ] DLL файл присутствует в архиве
- [ ] manifest.json содержит правильную версию (1.0.2)
- [ ] README.md актуален
- [ ] Все файлы находятся в правильных папках
