using System;
using System.ComponentModel;

[Serializable]
public class TakeItemAction : IStateAction {
    public SOItemInfo item;
    [DefaultValue(1)] public int count;
    
    public void Execute() {
        GameManager.instance.saveState.inventory.RemoveItem(item, count);
    }
}