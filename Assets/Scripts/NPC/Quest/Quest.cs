using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes a quest definition.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest Definition`", order = 0)]
public class Quest : ScriptableObject {
    [Tooltip("A short-form name for the quest.")]
    public string name = "New Quest";
    [Tooltip("A list of criteria for the quest to be completed.")]
    [SerializeReference] [PickImpl(typeof(IStatePredicate))] public List<IStatePredicate> criteria;

    /// <summary>
    /// Checks if the quest is completed.
    /// </summary>
    public bool IsCompleted() {
        foreach (var criterion in criteria) {
            if (!criterion.Check()) return false;
        }
        return true;
    }
}