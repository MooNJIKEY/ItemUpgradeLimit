@echo off
echo Компиляция Item Upgrade Mod для Valheim...
echo.

REM Проверка наличия dotnet
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ОШИБКА: .NET SDK не найден!
    echo Пожалуйста, установите .NET SDK 6.0 или новее
    echo Скачать можно здесь: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

cd ItemUpgradeMod

echo Восстановление зависимостей...
dotnet restore

echo.
echo Компиляция проекта...
dotnet build --configuration Release

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Компиляция успешно завершена!
    echo ========================================
    echo.
    echo Мод скопирован в папку плагинов BepInEx
    echo Запустите игру для генерации файла конфигурации
    echo.
) else (
    echo.
    echo ========================================
    echo ОШИБКА при компиляции!
    echo ========================================
    echo Проверьте вывод выше для деталей
    echo.
)

cd ..
pause

