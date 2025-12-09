# Game Scripts Documentation

This folder contains the core gameplay scripts for the project, managing game flow, UI, player stats, and the shop system.

## Game Management
- **GameManager.cs**: The central singleton manager for the game.
    - Tracks global state like the selected weapon index and total coins collected.
    - Persists across scenes to maintain player progress.
- **LevelSelect.cs**: Handles scene transitions from the main menu or level selection screen.
    - Provides methods to load the game level or exit the application.
- **CharacterSelect.cs**: Manages the character/weapon selection screen.
    - Allows players to cycle through available weapons/characters.
    - Updates 3D previews of the selected item.
    - Saves the selection to `GameManager` and `PlayerPrefs` before starting the game.

## User Interface (UI)
- **MainMenuUI.cs**: Simple script for the Main Menu scene.
    - Ensures the cursor is visible and unlocked.
    - Handles the "Exit Game" functionality.
- **PauseMenu.cs**: Manages the in-game pause functionality.
    - Toggles the pause menu panel.
    - Pauses and resumes `Time.timeScale`.
    - Handles returning to the main menu.
- **PlayerStatsDisplay.cs**: Controls the display of player statistics.
    - Toggles a UI panel showing health, damage multipliers, coin bonuses, and regeneration rates.
    - Updates the text dynamically based on current `PlayerStats` and `FPSPlayer` values.
- **TurnOffObject.cs**: A utility script with a single method `turnOffObject()` to deactivate the GameObject it is attached to (likely used by UI events or animations).

## Player Systems
- **PlayerStats.cs**: A singleton holding persistent player attributes.
    - Stores modifiers for damage, coin drop chances, and health regeneration settings.
    - Acts as a central data store for upgrades purchased in the shop.
- **PlayerWeaponManager.cs**: Handles spawning the player's selected weapon at the start of a level.
    - Reads the selected weapon index from `GameManager`.
    - Instantiates the corresponding weapon and ammo prefabs at the spawn point.

## Shop System
- **ShopManager.cs**: Manages the in-game shop interface and logic.
    - Generates random items for sale based on rarity (Common, Uncommon, Rare).
    - Handles purchasing logic, deducting coins and applying item effects.
    - Applies upgrades to `PlayerStats` (damage, regen, coins) or `FPSPlayer` (max health).
- **ShopItem.cs**: A `ScriptableObject` definition for shop items.
    - Defines item properties: Name, Description, Rarity, Cost, and a list of Effects (UpgradeType and Value).

## Testing
- **test.cs**: A temporary or debug script used to test button functionality and key bindings.
