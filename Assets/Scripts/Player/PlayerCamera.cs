using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using a camera parented to the player was giving some buggy behavior so the
// camera is its own object that moves and rotates with the PlayerLook object
// in LateUpdate, after mouse movement is computed.
public class PlayerCamera : MonoBehaviour
{
    private Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
        GameManager.instance.events.optionsReloadEvent.AddListener(UpdateFov);
        
        UpdateFov();
    }

    private void UpdateFov() {
        // Update the camera's field of view from the game options
        camera.fieldOfView = GameManager.instance.gameOptions.fieldOfView.Value;
    }
}
