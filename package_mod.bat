@echo off
echo ========================================
echo   Упаковка ItemUpgradeMod для публикации
echo ========================================
echo.

cd ItemUpgradeMod\ThunderstorePackage

echo Проверка файлов...

if not exist "ItemUpgradeMod.dll" (
    echo ❌ ОШИБКА: ItemUpgradeMod.dll не найден!
    echo    Запустите сначала build_mod.bat
    pause
    exit /b 1
)

if not exist "manifest.json" (
    echo ❌ ОШИБКА: manifest.json не найден!
    pause
    exit /b 1
)

if not exist "README.md" (
    echo ❌ ОШИБКА: README.md не найден!
    pause
    exit /b 1
)

if not exist "icon.png" (
    echo.
    echo ⚠️  ВНИМАНИЕ: icon.png не найден!
    echo.
    echo Пожалуйста, создайте иконку 256x256 пикселей:
    echo 1. Используйте онлайн редактор: https://www.canva.com/ или https://www.photopea.com/
    echo 2. Создайте изображение 256x256 пикселей
    echo 3. Нарисуйте что-то связанное с улучшением предметов
    echo 4. Сохраните как icon.png в эту папку
    echo 5. Запустите этот скрипт снова
    echo.
    pause
    exit /b 1
)

echo.
echo ✅ Все файлы найдены!
echo.
echo Создание ZIP архива...

cd ..\..\

if exist "ItemUpgradeMod_v1.0.1.zip" del "ItemUpgradeMod_v1.0.1.zip"

powershell -Command "Compress-Archive -Path 'ItemUpgradeMod\ThunderstorePackage\*' -DestinationPath 'ItemUpgradeMod_v1.0.1.zip' -Force"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo ✅ Пакет создан успешно!
    echo ========================================
    echo.
    echo Файл: ItemUpgradeMod_v1.0.1.zip
    echo.
    echo Содержимое:
    dir /B ItemUpgradeMod\ThunderstorePackage
    echo.
    echo Готов к загрузке на:
    echo - Thunderstore: https://valheim.thunderstore.io/
    echo - Nexus Mods: https://www.nexusmods.com/valheim
    echo.
) else (
    echo.
    echo ❌ ОШИБКА при создании ZIP!
    echo.
)

pause

