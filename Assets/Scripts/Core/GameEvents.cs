using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Contains events for core gameplay.
/// </summary>
public class GameEvents {
    /// <summary>
    /// Invoked when the game's options are saved.
    /// </summary>
    public readonly UnityEvent optionsReloadEvent = new UnityEvent();
    
    /// <summary>
    /// Invoked when the game state is changed.
    /// </summary>
    public readonly UnityEvent<GameManager.GameState> gameStateChangeEvent = new UnityEvent<GameManager.GameState>();
}
