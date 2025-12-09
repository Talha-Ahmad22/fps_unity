# Objects Scripts Documentation

This folder contains scripts related to various interactive world objects, environmental hazards, and utility systems.

## General Objects
- **AzuObjectPool.cs**: A robust object pooling system. It manages pools of objects (like bullets, particles, decals) to reuse them instead of constantly instantiating and destroying them, which improves performance.
- **DamageZone.cs**: Applies damage to the player or NPCs when they stay within a trigger area (e.g., fire, acid, spikes).
- **FenceRustle.cs**: Plays specific rustling sound effects when the player collides with a fence object.
- **FoliageRustle.cs**: Plays foliage rustling sounds when the player moves through vegetation.
- **InstantDeathCollider.cs**: Instantly kills the player or destroys objects that touch it (e.g., bottomless pits, kill zones).
- **ObjRandomSpawnChance.cs**: A utility to randomly destroy an object at start based on a probability chance, adding variation to the environment.
- **WaterZone.cs**: A comprehensive script for handling water physics and effects. It manages:
    - Swimming and diving mechanics.
    - Underwater visual effects (fog, lighting, caustics).
    - Splash sounds and particles.
    - Oxygen/breath holding.
- **WorldRecenter.cs**: (Experimental/Legacy) Attempts to re-center the world origin if the player travels too far, to prevent floating-point precision errors in very large maps.

## Climbing (!Climbing)
- **EdgeClimbTrigger.cs**: Detects when a jumping player hits a ledge and applies an upward force to help them vault over it.
- **EdgeClimbTriggerActivate.cs**: Reactivates edge climb triggers that may have been disabled, ensuring they are ready for the player to use again.
- **Ladder.cs**: Allows the player to climb vertical surfaces. It handles the climbing state, movement logic, and sound effects.

## Destructibles (!Destructibles)
- **BreakableObject.cs**: Makes an object breakable. It tracks health, spawns particle effects upon breaking, and handles the cleanup of the broken object.
- **ExplosiveObject.cs**: Handles objects that explode when damaged (e.g., barrels, gas tanks). It deals area damage and applies physics force to nearby objects.
- **MineExplosion.cs**: Logic for landmines. It detects players or NPCs entering its radius, plays a warning beep, and then detonates, dealing damage and force.

## Platforms (!Platforms)
- **MovingElevator.cs**: Moves an object vertically between two points.
- **MovingPlatform.cs**: Moves an object horizontally between two points.
- **ElevatorCrushCollider.cs**: Attached to the bottom of elevators to instantly kill the player if they get crushed underneath.
