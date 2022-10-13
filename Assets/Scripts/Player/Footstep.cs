using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    public AudioSource footstepsSound, sprintSound;
    private PlayerMovement player;

    void Start() {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update() {
        if (GameManager.instance.gameState != GameManager.GameState.Playing && player.playerData.isOnGround && player.horizontalVelocity.magnitude >= .1f) {
            switch (player.playerData.playerState) {
                case PlayerMovement.PlayerState.Sprinting:
                    footstepsSound.enabled = false;
                    sprintSound.enabled = true;
                    break;
                default:
                    footstepsSound.enabled = true;
                    sprintSound.enabled = false;
                    break;
            }
        }
        else {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }
    }
}