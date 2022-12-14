using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes a dialogue interaction tree.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Dialogue Tree", menuName = "Dialogue Tree", order = 0)]
public class DialogueTree : ScriptableObject {
    public Sprite portrait;
    public DialogueNode entryPoint;

    public DialogueContext Traverse(Action<string> refreshNpcText, Action<string[]> refreshOptions, Action<Sprite> changePortrait, Action onDialogueEnd) {
        return new DialogueContext(this, refreshNpcText, refreshOptions, changePortrait, onDialogueEnd);
    }
}