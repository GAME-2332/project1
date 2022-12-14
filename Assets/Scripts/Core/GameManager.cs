using UnityEngine;

/// <summary>
/// A singleton class containing core game info and functionality - options, save states, etc.
/// Should persist through scenes.
/// </summary>
public class GameManager {
    public static readonly GameManager instance = new GameManager();

    // TODO: Once we load scenes from the main menu, initialize with the MaiNMenu state
    private GameState _gameState = GameState.Playing;
    public GameState gameState { get => _gameState; set {
        _gameState = value;
        Cursor.lockState = _gameState == GameState.Playing ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = _gameState != GameState.Playing;
        events.gameStateChangeEvent.Invoke(_gameState);
    }}
    public GameEvents events = new GameEvents();
    public GameOptions gameOptions = new GameOptions();
    // Both saveState and globals are assigned when a save is loaded
    public SaveState saveState;

    public void LoadOrCreateSave(int slot) {
        // Failsafe in case we're loading a new save while already in a save.
        if (saveState != null) saveState.SaveCurrent();
        saveState = new SaveState(slot);
        saveState.Load();
        
        ItemRegistry.Wake();
    }

    public enum GameState {
        MainMenu,
        Playing,
        Paused,
        Inventory,
        Dialogue
    }
}
