# Player Scripts Documentation

This folder contains the core scripts that define the player's behavior, movement, input, and visual representation.

## Core Player Logic
- **FPSPlayer.cs**: The central manager for the player entity. It handles:
    - **Player Stats**: Health, hunger, thirst, and regeneration logic.
    - **HUD Integration**: Updates UI elements for health, ammo, crosshairs, and hitmarkers.
    - **Interaction**: Manages raycasting for picking up items and interacting with the world.
    - **Game State**: Handles pausing, bullet time (slow motion), and level transitions.
    - **Coordination**: Acts as a hub connecting input, movement, and weapon systems.

## Movement & Physics
- **FPSRigidBodyWalker.cs**: Controls the physics-based movement of the player character. Key features include:
    - **Movement Modes**: Walking, sprinting, crouching, prone, swimming, and climbing.
    - **Physics Interaction**: Applies forces to the Rigidbody for jumping and movement, handling slope limits and ground detection.
    - **Stance Management**: Transitions between standing, crouching, and prone states, adjusting collider sizes accordingly.
    - **Surface Interaction**: Detects water and ladders to switch movement logic.

## Input System
- **InputControl.cs**: A centralized input manager that abstracts hardware input (Keyboard, Mouse, Gamepad) into game actions.
    - Reads Unity's Input Manager axes and buttons.
    - Exposes public state variables (e.g., `firePress`, `jumpHold`, `moveX`) for other scripts to consume.
    - Handles input smoothing and acceleration.

## Audio & Visuals
- **Footsteps.cs**: Manages audio feedback for movement.
    - Detects the surface material (Texture on Terrain or Tag on GameObjects) under the player.
    - Plays appropriate sound effects for different movement types (walk, sprint, crouch, climb, swim).
- **PlayerCharacter.cs**: Manages the visual body of the player.
    - **Third-Person Model**: Animates the full character model for third-person views or shadows.
    - **First-Person Body**: Handles the "visible body" (legs/torso) seen in first-person view.
    - **Animation Sync**: Synchronizes Mecanim animator parameters with the player's movement speed, direction, and stance.

## Utilities
- **ReconfigurePrefab.cs**: A configuration tool to toggle between Single-Camera and Dual-Camera setups.
    - **Dual Camera**: Renders weapons on a separate camera to prevent clipping into walls (good for large scenes).
    - **Single Camera**: Uses one camera for everything (simpler, better for lighting consistency in some cases).
- **LeanColliderDamage.cs**: A helper script attached to the "leaning" collider object. It ensures that if the player leans out and gets hit, the damage is correctly applied to the main `FPSPlayer` health.
