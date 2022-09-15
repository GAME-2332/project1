using System;
using UnityEngine;

/// <summary>
/// Defines data for the player to be serialized and passed between scenes.
/// Separate from PlayerMovement to avoid passing references back and forth.
/// </summary>
[Serializable]
public struct PlayerData {
    public Vector3 velocity;
    public float xRot;
    public float yRot;
    public PlayerMovement.PlayerState playerState;
    public bool isOnGround;
}
