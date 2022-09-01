using UnityEngine;

/// <summary>
/// A singleton class containing core game info and functionality - options, save states, etc.
/// Should persist through scenes.
/// </summary>
public class GameManager {
    public static readonly GameManager instance = new GameManager();

    public GameEvents events = new GameEvents();
    public GameOptions gameOptions = new GameOptions();
    public bool usingJoystickControls = false;
}
