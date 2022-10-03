
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quests : ISerializationCallbackReceiver {
    // TODO : The entire quest implementation is a mess, but it works and we don't have time to fix it right now.
    public static Dictionary<string, Quest> questMap = Util.LoadResources<Quest>("Quests");
    
    [SerializeField] private List<string> completedQuestNames = new();
    private List<Quest> completedQuests = new();
    
    public bool IsCompleted(Quest quest) {
        return completedQuests.Contains(quest);
    }

    public void Complete(Quest quest) {
        completedQuests.Add(quest);
    }

    public void OnBeforeSerialize() {
        completedQuestNames.Clear();
        foreach (var quest in completedQuests) {
            completedQuestNames.Add(quest.name);
        }
    }
    
    public void OnAfterDeserialize() {
        completedQuests.Clear();
        foreach (var questName in completedQuestNames) {
            var quest = questMap[questName];
            if (quest != null) completedQuests.Add(quest);
        }
    }
}