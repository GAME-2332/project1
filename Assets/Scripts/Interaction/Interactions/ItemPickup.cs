using UnityEngine;

[RequireComponent(typeof(SaveMe))]
public class ItemPickup : Interactible {
    public ItemInfo item;

    public override void Interact() {
        GameManager.instance.saveState.heldItems.Add(item);
        Destroy(gameObject);
    }
}
