using System;
using System.ComponentModel;

/// <summary>
/// Checks if the player has at least a certain number of a given item.
/// </summary>
[Serializable]
public class HasItemPredicate : IStatePredicate {
    public SOItemInfo item;
    [DefaultValue(1)] public int count;
    
    public bool Check() {
        return GameManager.instance.saveState.inventory.HasItem(item, count);
    }
}