using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInventory {
    [SerializeReference] private List<SOItemInfo> items = default;
    
    public int Count => items.Count;
    
    public SOItemInfo GetItem(int index) {
        return items[index];
    }
    
    public void AddItem(SOItemInfo item) {
        items.Add(item);
    }
    
    public void RemoveItem(SOItemInfo item) {
        items.Remove(item);
    }
    
    public bool HasItem(SOItemInfo item) {
        return items.Contains(item);
    }
}