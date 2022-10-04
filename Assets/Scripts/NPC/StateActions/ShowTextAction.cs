using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Shows text to the action bar.
/// </summary>
[Serializable]
public class ShowTextAction : IStateAction {
    public string text;
    public float duration;

    public void Execute() {
        ActionBar.Show(text, duration);
    }
}