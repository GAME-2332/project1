using UnityEngine;

[RequireComponent(typeof(SaveMe))]
public class ItemPickup : Interactible {
    public SOItemInfo item;
    public AudioClip pickupSound;

    public override void Interact() {
        GameManager.instance.saveState.inventory.AddItem(item);
        if (pickupSound != null) {
            // Set the right sound and play it
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        Destroy(gameObject);
    }
}
