using System;

/// <summary>
/// Checks if the player has a given item.
/// </summary>
[Serializable]
public class ItemPredicate : IStatePredicate {
    public SOItemInfo item;
    
    public bool Check() {
        return GameManager.instance.saveState.heldItems.Contains(item);
    }
}