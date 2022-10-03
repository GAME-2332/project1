using System;
using UnityEngine.Events;

/// <summary>
/// Runs a custom Action delegate as defined in the inspector.
/// </summary>
[Serializable]
public class CompleteQuestAction : IStateAction {
    public Quest quest;

    public void Execute() => GameManager.instance.saveState.quests.Complete(quest);
}