#!/bin/bash
echo "Компиляция Item Upgrade Mod для Valheim..."
echo ""

# Проверка наличия dotnet
if ! command -v dotnet &> /dev/null; then
    echo "ОШИБКА: .NET SDK не найден!"
    echo "Пожалуйста, установите .NET SDK 6.0 или новее"
    echo "Скачать можно здесь: https://dotnet.microsoft.com/download"
    exit 1
fi

cd ItemUpgradeMod

echo "Восстановление зависимостей..."
dotnet restore

echo ""
echo "Компиляция проекта..."
dotnet build --configuration Release

if [ $? -eq 0 ]; then
    echo ""
    echo "========================================"
    echo "Компиляция успешно завершена!"
    echo "========================================"
    echo ""
    echo "Мод скопирован в папку плагинов BepInEx"
    echo "Запустите игру для генерации файла конфигурации"
    echo ""
else
    echo ""
    echo "========================================"
    echo "ОШИБКА при компиляции!"
    echo "========================================"
    echo "Проверьте вывод выше для деталей"
    echo ""
fi

cd ..

