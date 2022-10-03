
using System;
using System.Collections.Generic;

[Serializable]
public class Quests {
    public List<Quest> completedQuests = new List<Quest>();
    
    public bool IsCompleted(Quest quest) {
        if (completedQuests.Contains(quest)) return true;
        if (quest.IsSatisfied()) {
            completedQuests.Add(quest);
            return true;
        }

        return false;
    }
}