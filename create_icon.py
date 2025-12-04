#!/usr/bin/env python3
"""Simple script to create a 256x256 icon for Thunderstore"""
from PIL import Image, ImageDraw, ImageFont
import os

# Create a 256x256 image
img = Image.new('RGB', (256, 256), color=(50, 100, 150))
draw = ImageDraw.Draw(img)

# Draw a simple upgrade symbol (arrow up with plus)
# Draw a circle
draw.ellipse([30, 30, 226, 226], fill=(70, 130, 180), outline=(255, 255, 255), width=4)

# Draw an up arrow
points = [(128, 80), (100, 120), (110, 120), (110, 160), (146, 160), (146, 120), (156, 120)]
draw.polygon(points, fill=(255, 255, 255))

# Draw a plus sign at the bottom
draw.rectangle([108, 170, 148, 200], fill=(255, 200, 0))
draw.rectangle([118, 160, 138, 210], fill=(255, 200, 0))

# Save the icon
output_path = os.path.join('ItemUpgradeMod', 'ThunderstorePackage', 'icon.png')
img.save(output_path, 'PNG')
print(f'Icon created: {output_path}')
