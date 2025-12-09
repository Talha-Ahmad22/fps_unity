# Camera Scripts Documentation

This folder contains scripts related to the camera system, including player view control, camera animations, object interaction, and audio utilities.

## Camera Control & Movement
- **CameraControl.cs**: The core script for managing the camera. It handles:
    - Positioning and rotation for both First-Person and Third-Person views.
    - Camera bobbing and swaying effects.
    - Transitions between camera modes.
    - Interaction with weapon animations and recoil.
- **SmoothMouseLook.cs**: Handles mouse input to rotate the camera (look around). It includes features for:
    - Smoothing mouse movement.
    - Clamping vertical angles.
    - Applying recoil to the view.
    - Managing cursor visibility and locking.
- **MovePlayerAndCamera.cs**: A utility script for:
    - Teleporting the player and camera to specific locations.
    - Toggling between the main player camera and external "cinematic" cameras.
    - Releasing camera control for cutscenes.

## Animation & Effects
- **CamAndWeapAnims.cs**: A data container script that holds vector values modified by Unity's animation system. These values are read by `CameraControl.cs` to apply keyframed bobbing and swaying effects to the camera and weapon.

## Interaction
- **DragRigidbody.cs**: Allows the player to interact with physics objects in the world.
    - Raycasts to find rigidbodies.
    - Uses a `SpringJoint` to drag objects around.
    - Handles throwing objects.

## Audio Utilities
- **PlayAudioAtPos.cs**: A static helper class for playing 3D sounds at a specific position. It uses an object pool to spawn temporary audio sources.
- **TempAudioTimer.cs**: Attached to the temporary audio objects spawned by `PlayAudioAtPos`. It waits for the audio clip to finish playing and then deactivates the object so it can be returned to the pool.
