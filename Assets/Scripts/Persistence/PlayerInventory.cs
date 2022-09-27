using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerInventory : ISerializationCallbackReceiver {
    private List<SOItemInfo> items = new List<SOItemInfo>();
    [SerializeField] private string[] itemNames;
    
    public int Count => items.Count;

    public void OnBeforeSerialize() {
        itemNames = items.Select(s => s.name).ToArray();
    }

    public void OnAfterDeserialize() {
        foreach (string name in itemNames) {
            items.Add(ItemRegistry.GetItem(name));
        }
    }
    
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