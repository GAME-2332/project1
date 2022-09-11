# SaveState system

This directory contains the game save/loading system. Tentative implementation plan is as below:

## SaveState
- Object instance under GameManager, created at startup for the selected save slot
- Stores the actual current game's save state for a given slot
- Holds any non-scene-attached data between scenes (inventory, etc)
- When a scene is loaded, writes the last one to disk and sets up the now active SceneData for serialization; if data exists on the disk for this save slot it loads that to the SceneData object
- Stores a reference to only one active SceneData object at a time
- Contains a `LoadScene` method that must be used in place of `SceneManager#LoadScene`

## SceneData
- Component attached to one object in each scene
- Defines which data needs to be serialized within the scene
- Maps game objects by unique identifiers
- Stores a list of game obejcts that have been destroyed to destroy again if the scene is deserialized