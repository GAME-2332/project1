using UnityEngine;

/// <summary>
/// A singleton class containing core game info and functionality - options, save states, etc.
/// Should persist through scenes.
/// </summary>
public class GameManager {
    public static readonly GameManager instance = new GameManager();

    public SaveState saveState;
    public GameEvents events = new GameEvents();
    public GameOptions gameOptions = new GameOptions();

    public void LoadOrCreateSave(int slot) {
        // Failsafe in case we're loading a new save while already in a save.
        if (saveState != null) saveState.SaveCurrent();
        saveState = new SaveState(slot);
        saveState.Load();
    }
}
