using System;
using UnityEngine.Events;

/// <summary>
/// Runs a custom Action delegate as defined in the inspector.
/// </summary>
[Serializable]
public class CustomAction : IStateAction {
    public UnityEvent action;

    public void Execute() => action.Invoke();
}