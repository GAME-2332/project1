using System;

[Serializable]
public class QuestCompletePredicate : IStatePredicate {
    public Quest quest;
    
    public bool Check() {
        return quest.IsCompleted();
    }
}