using System;
using UnityEngine;

/// <summary>
/// Defines data for the player to be serialized and passed between scenes.
/// Separate from PlayerMovement to avoid passing references back and forth.
/// </summary>
[Serializable]
public class PlayerData : MonoBehaviour, ISerializationCallbackReceiver {
    public Vector3 velocity;
    public float xRot;
    public float yRot;
    public PlayerMovement.PlayerState playerState;
    public bool isOnGround;

    private PlayerMovement playerMovement;

    public void OnBeforeSerialize() {
        playerMovement = GetComponent<PlayerMovement>();
        velocity = playerMovement.velocity;
        xRot = playerMovement.xRot;
        yRot = playerMovement.yRot;
        playerState = playerMovement.playerState;
        isOnGround = playerMovement.isOnGround;
    }

    public void OnAfterDeserialize() {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.velocity = velocity;
        playerMovement.xRot = xRot;
        playerMovement.yRot = yRot;
        playerMovement.playerState = playerState;
        playerMovement.isOnGround = isOnGround;
    }
}