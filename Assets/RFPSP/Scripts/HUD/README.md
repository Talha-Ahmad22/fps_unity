# HUD Scripts Documentation

This folder contains scripts related to the Heads-Up Display (HUD) and User Interface (UI) elements of the game.

## Player Status Indicators
- **AmmoText.cs**: Displays the current weapon's ammo count (clip / total).
- **HealthText.cs**: Shows the player's current health points.
- **HungerText.cs**: Displays the player's hunger level (if enabled).
- **ThirstText.cs**: Displays the player's thirst level (if enabled).

## Game State & Information
- **HelpText.cs**: Shows a list of game controls on screen. It fades out after a set time or when the player moves, but can be toggled with a key press (F1).
- **WarmupText.cs**: Displays the countdown timer before a wave begins, as well as "Incoming Wave" and "Wave Complete" messages.
- **WaveText.cs**: Shows the current wave number and the number of enemies remaining in the wave.

## Menus
- **MainMenu.cs**: Manages the main menu interface. It handles:
    - Resuming the game.
    - Restarting the map.
    - Toggling settings like Invulnerability, Hunger/Thirst, and Free Aim Zooming.
    - Giving all weapons (cheat/debug).
    - Exiting the game.
