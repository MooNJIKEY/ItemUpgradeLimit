#!/bin/bash

echo "========================================"
echo "  Упаковка ItemUpgradeMod для публикации"
echo "========================================"
echo ""

cd ItemUpgradeMod/ThunderstorePackage

echo "Проверка файлов..."

if [ ! -f "ItemUpgradeMod.dll" ]; then
    echo "❌ ОШИБКА: ItemUpgradeMod.dll не найден!"
    echo "   Запустите сначала build_mod.sh"
    exit 1
fi

if [ ! -f "manifest.json" ]; then
    echo "❌ ОШИБКА: manifest.json не найден!"
    exit 1
fi

if [ ! -f "README.md" ]; then
    echo "❌ ОШИБКА: README.md не найден!"
    exit 1
fi

if [ ! -f "icon.png" ]; then
    echo ""
    echo "⚠️  ВНИМАНИЕ: icon.png не найден!"
    echo ""
    echo "Пожалуйста, создайте иконку 256x256 пикселей:"
    echo "1. Используйте онлайн редактор: https://www.canva.com/ или https://www.photopea.com/"
    echo "2. Создайте изображение 256x256 пикселей"
    echo "3. Нарисуйте что-то связанное с улучшением предметов"
    echo "4. Сохраните как icon.png в эту папку"
    echo "5. Запустите этот скрипт снова"
    echo ""
    exit 1
fi

echo ""
echo "✅ Все файлы найдены!"
echo ""
echo "Создание ZIP архива..."

cd ../../

rm -f ItemUpgradeMod_v1.0.1.zip

zip -j ItemUpgradeMod_v1.0.1.zip ItemUpgradeMod/ThunderstorePackage/*

if [ $? -eq 0 ]; then
    echo ""
    echo "========================================"
    echo "✅ Пакет создан успешно!"
    echo "========================================"
    echo ""
    echo "Файл: ItemUpgradeMod_v1.0.1.zip"
    echo ""
    echo "Содержимое:"
    ls -1 ItemUpgradeMod/ThunderstorePackage/
    echo ""
    echo "Готов к загрузке на:"
    echo "- Thunderstore: https://valheim.thunderstore.io/"
    echo "- Nexus Mods: https://www.nexusmods.com/valheim"
    echo ""
else
    echo ""
    echo "❌ ОШИБКА при создании ZIP!"
    echo ""
fi

