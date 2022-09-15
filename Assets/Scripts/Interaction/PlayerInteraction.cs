using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public Transform cam;
    public float playerActivateDistance;

    private Interactible lastHit;
    private bool active = false;

    private void Update() {
        // Don't listen for interactions if the game isn't playing
        if (GameManager.instance.gameState != GameManager.GameState.Playing) return;
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance);
        if (active == true) {
            if (hit.transform.GetComponent<Interactible>() != null) {
                lastHit = hit.transform.GetComponent<Interactible>();
                lastHit.SetOutline(true);
                if (GameManager.instance.gameOptions.interact.GetKeyDown()) {
                    lastHit.Interact();
                }
            } else if (lastHit != null) lastHit.SetOutline(false);
        } else {
            if (lastHit != null) lastHit.SetOutline(false);  
        }       
    }
}
