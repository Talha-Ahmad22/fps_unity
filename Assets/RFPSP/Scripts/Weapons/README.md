# Weapons Scripts Documentation

This folder contains scripts related to weapon functionality, including firing, reloading, aiming, recoil, and weapon switching.

## Core Weapon Logic
- **WeaponBehavior.cs**: The comprehensive script attached to each weapon object. It handles:
    - **Firing**: Raycast or projectile-based shooting, fire rates, burst modes, and melee attacks.
    - **Ammo**: Magazine management, reloading logic, and ammo inventory.
    - **Visuals**: Muzzle flashes, smoke, tracers, and shell ejection spawning.
    - **Recoil**: Applies procedural recoil to the camera and weapon accuracy.
    - **Animations**: Triggers animations for firing, reloading, and sprinting.
    - **Zooming**: Handles FOV changes and weapon positioning for ADS (Aim Down Sights).
- **PlayerWeapons.cs**: Manages the player's weapon inventory.
    - **Switching**: Handles input for switching weapons (keys, mouse wheel) and the switching coroutines.
    - **Inventory**: Tracks which weapons are owned and their current ammo.
    - **Dropping**: Logic for dropping the current weapon and spawning its pickup object.
    - **Grenades**: Manages offhand grenade throwing and switching between grenade types.

## Aiming & Positioning
- **Ironsights.cs**: Manages the precise positioning of the weapon model.
    - **ADS**: Smoothly interpolates the weapon to the "Zoom" position when aiming.
    - **Bobbing**: Applies procedural bobbing to the weapon and camera based on movement state (walk, sprint, crouch, swim).
    - **Sway**: Handles weapon sway based on mouse input and movement.
- **WeaponPivot.cs**: Handles weapon rotation around a specific pivot point.
    - **Deadzone Aiming**: Implements "GoldenEye/Perfect Dark" style free aiming where the weapon moves independently of the camera within a "deadzone".
    - **Sway Leading**: Rotates the weapon to "lead" the turn direction for a more dynamic feel.
- **GunSway.cs**: Adds procedural sway and lag to the weapon model.
    - **Mouse Sway**: Tilts and moves the gun based on mouse look input.
    - **Movement Sway**: Adds rolling and bobbing motions based on player movement.

## Projectiles & Physics
- **ArrowObject.cs**: Logic for arrow projectiles.
    - **Flight**: Aligns rotation with velocity for realistic flight arcs.
    - **Impact**: Detects collisions, applies damage, and "sticks" to objects by parenting itself to them.
    - **Retrieval**: Allows the player to pick the arrow back up.
- **GrenadeObject.cs**: Simple script for grenade projectiles.
    - **Detonation**: Waits for a fuse timer and then calls `ExplosiveObject.ApplyDamage` to explode.
- **ShellEjection.cs**: Manages ejected bullet casings.
    - **Physics**: Applies initial force and torque to the shell rigidbody.
    - **Audio**: Plays bounce sounds when colliding with the environment.
    - **Cleanup**: Recycles the shell object back to the pool after a set duration.

## Data & Utilities
- **ThirdPersonWeapons.cs**: A data container for third-person weapon references.
    - Stores links to the third-person mesh, offhand weapons, and muzzle flash/smoke transforms for the third-person character model.
