using System;

[Serializable]
public class TakeItemAction : IStateAction {
    public SOItemInfo item;
    
    public void Execute() {
        GameManager.instance.saveState.heldItems.Remove(item);
    }
}