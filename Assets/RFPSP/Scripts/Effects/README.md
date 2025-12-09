# Effects Scripts Documentation

This folder contains scripts related to visual and audio effects, including weapon impacts, screen fades, and decal management.

## Weapon & Impact Effects
- **WeaponEffects.cs**: The central manager for weapon-related visual feedback. It handles:
    - **Impact Particles**: Spawns specific particle effects based on the tag of the object hit (e.g., Dirt, Metal, Flesh, Wood, Water).
    - **Sound Effects**: Plays material-specific impact sounds.
    - **Bullet Decals**: Places and orients bullet hole decals on surfaces, ensuring they scale correctly even on non-uniformly scaled objects.
    - **Tracers**: Emits bullet tracer particles for both the player and NPCs.
    - **Explosions**: Triggers explosion particle effects.

## Decal Management
- **FadeOutDecals.cs**: Attached to decal objects (like bullet holes) to handle their lifecycle. It fades them out over time and returns them to the object pool or deactivates them to keep the scene clean.

## Screen Fades
- **LevelLoadFade.cs**: Manages full-screen fades (usually to black) during level transitions or loading sequences.
- **PainFade.cs**: Flashes the screen (typically red) to indicate that the player has taken damage.
