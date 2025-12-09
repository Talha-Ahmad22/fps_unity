# Items Scripts Documentation

This folder contains scripts related to pickup items, including weapons, ammo, health, and food/drink.

## Weapon & Ammo Pickups
- **WeaponPickup.cs**: Allows the player to pick up weapons. It handles:
    - Equipping the new weapon.
    - Dropping the current weapon if inventory is full.
    - Adding ammo if the player already has the weapon.
- **AmmoPickup.cs**: Adds ammo to a specific weapon type when picked up.
- **WeaponSpawn.cs**: A simple spawner that periodically creates a weapon pickup at its location (e.g., for multiplayer or arena maps).

## Consumables
- **HealthPickup.cs**: Restores player health when picked up.
- **FoodPickup.cs**: Restores hunger and a small amount of health.
- **DrinkPickup.cs**: Restores thirst and a small amount of health.

## Interactive Objects
- **AppleFall.cs**: Attached to apple objects on trees. Makes them fall (enable gravity) when damaged or "picked" by the player.
