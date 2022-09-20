using System;

[Serializable]
public class GiveItemAction : IStateAction {
    public SOItemInfo item;
    
    public void Execute() {
        GameManager.instance.saveState.heldItems.Add(item);
    }
}