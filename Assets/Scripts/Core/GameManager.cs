using UnityEngine;

/// <summary>
/// A singleton class containing core game info and functionality - options, save states, etc.
/// Should persist through scenes.
/// </summary>
public class GameManager {
    public static readonly GameManager instance = new GameManager();

    public GameState gameState = GameState.MainMenu;
    public GameEvents events = new GameEvents();
    public GameOptions gameOptions = new GameOptions();
    // Both saveState and globals are assigned when a save is loaded
    public SaveState saveState;
    public Globals globals;

    public void LoadOrCreateSave(int slot) {
        // Failsafe in case we're loading a new save while already in a save.
        if (saveState != null) saveState.SaveCurrent();
        saveState = new SaveState(slot);
        saveState.Load();
    }

    public enum GameState {
        MainMenu,
        Playing,
        Paused,
        Inventory,
        Dialogue
    }
}
