using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Describes a quest definition.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest Definition`", order = 0)]
public class Quest : ScriptableObject {
    [FormerlySerializedAs("name")] [Tooltip("A short-form name for the quest.")]
    public string questName = "New Quest";

    /// <summary>
    /// Checks if the quest is completed.
    /// </summary>
    public bool IsCompleted() {
        return GameManager.instance.saveState.quests.IsCompleted(this);
    }
}