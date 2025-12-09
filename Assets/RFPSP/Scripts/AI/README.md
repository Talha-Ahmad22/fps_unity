# AI Scripts Documentation

This folder contains scripts related to the AI system, including behavior, spawning, combat, and wave management.

## Core AI
- **AI.cs**: The main brain of the NPC. Handles navigation, state machine (Patrol, Attack, Follow), animation control, and target detection.
- **NPCRegistry.cs**: A global manager that tracks all active NPCs. It assists in target selection based on factions and distance.

## Combat & Damage
- **NPCAttack.cs**: Manages NPC weapon firing, including raycasting for shots, muzzle flashes, and sound effects.
- **CharacterDamage.cs**: Handles health and damage processing for the NPC. Manages death sequences (ragdolls) and loot drops.
- **LocationDamage.cs**: Attached to specific body parts (like the head) to apply damage multipliers (e.g., headshots) and pass damage to the main `CharacterDamage` script.

## Spawning & Waves
- **NPCSpawner.cs**: Handles spawning of NPCs. Can be used for continuous spawning or controlled by the `WaveManager`.
- **WaveManager.cs**: Manages game waves, controlling spawners to release enemies in increasing difficulty. Tracks kills and updates the UI.
- **RemoveBody.cs**: Automatically destroys dead NPC objects and dropped items after a delay to maintain performance.

## Triggers & Events
- **MonsterItemTrap.cs**: Activates specific NPCs when a player picks up a designated item, useful for ambushes.
- **MonsterTrigger.cs**: Activates specific NPCs when the player enters a trigger zone.
- **MoveTrigger.cs**: Commands a specific NPC to move to a target location when triggered. Useful for scripted sequences where an NPC leads the player.

## Utilities
- **WaypointGroup.cs**: Manages a collection of transforms that define a patrol path for NPCs. Visualizes these paths in the Unity Editor.
