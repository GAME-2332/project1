using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component providing basic movement controls for the player based on configured inputs from the GameManager.
/// Movement units are in units per second, thus are mostly divided by 50 (number of physics ticks per second).
/// </summary>
public class PlayerMovement : MonoBehaviour {
    [Header("Physics")]
    public Transform playerLook;
    [Tooltip("Base movement speed")]
    public float moveSpeed;
    [Range(1,3)]
    [Tooltip("Sprinting speed modifier")]
    public float sprintModifier;
    [Range(0,1)]
    [Tooltip("Crouching speed modifier")]
    public float crouchModifier;
    [Tooltip("Jump speed")]
    public float jumpSpeed;
    [Tooltip("Acceleration due to gravity")]
    public float fallSpeed;
    [Header("Controls")]
    public float mouseSensitivity;
    [Range(.5f, 1)]
    [Tooltip("The player's scale when crouching")]
    public float crouchHeight;
    [Header("Visuals")]
    [Range(0, 1)]
    [Tooltip("How much to smooth the crouching animation")]
    public float crouchSmoothing;
    [Tooltip("Distance of the focal point for view bobbing")]
    public float focalDistance;
    [Range(.0005f, .1f)]
    [Tooltip("View bobbing intensity when walking normally")]
    public float bobbingIntensity;
    [Range(2, 36)]
    [Tooltip("View bobbing speed when walking normally")]
    public float bobbingSpeed;
    [Range(.0005f, .1f)]
    [Tooltip("View bobbing intensity when sprinting")]
    public float bobbingIntensitySprinting;
    [Range(2, 36)]
    [Tooltip("View bobbing speed when sprinting")]
    public float bobbingSpeedSprinting;
    [Range(.0005f, .1f)]
    [Tooltip("View bobbing intensity when crouching")]
    public float bobbingIntensityCrouching;
    [Range(2, 36)]
    [Tooltip("View bobbing speed when crouching")]
    public float bobbingSpeedCrouching;

    // Serialized separately by SceneData to avoid overwriting inspector references at runtime
    [NonSerialized]
    public PlayerData playerData;

    private GameOptions gameOptions;
    private CharacterController controller;
    private Vector3 cameraFocus;
    private Vector3 initialScale;
    private Vector3 initialPlayerLook;
    private float groundDistance;

    void Start() {
        gameOptions = GameManager.instance.gameOptions;
        controller = GetComponent<CharacterController>();
        initialScale = transform.localScale;
        initialPlayerLook = playerLook.localPosition;
        groundDistance = GetComponent<Collider>().bounds.extents.y;
        // In case the scene wasn't loaded properly through SaveState, lock the cursor here
        // (temporary until we have a main menu hooked up)
        if (GameManager.instance.gameState == GameManager.GameState.Playing) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void FixedUpdate() {
        // Don't do anything if the game isn't playing
        if (GameManager.instance.gameState != GameManager.GameState.Playing) return;

        // Calculate movement in FixedUpdate, per physics tick
        // First, check if we're on the ground
        playerData.isOnGround = Physics.Raycast(transform.position, Vector3.down, groundDistance * transform.localScale.y + .1f);

        // Check if sprinting/crouching
        // TODO: Stamina
        bool canSprint = playerData.isOnGround;
        if (Input.GetKey(gameOptions.crouch.Value)) {
            playerData.playerState = PlayerState.Crouching;
        } else {
            if (canSprint && Input.GetKey(gameOptions.sprint.Value)) playerData.playerState = PlayerState.Sprinting;
            else playerData.playerState = PlayerState.Normal;
        }

        // Add player movement velocity
        Vector3 horizontalVelocity = (GetInputVector() * MoveSpeed() / 50);
        playerData.velocity.Set(horizontalVelocity.x, playerData.velocity.y, horizontalVelocity.z);

        // Crouching
        float targetHeight = playerData.playerState == PlayerState.Crouching ? crouchHeight : initialScale.y;
        if (transform.localScale.y != targetHeight) {
            float deltaHeight = (transform.localScale.y - targetHeight) * (crouchSmoothing - 1);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + deltaHeight, transform.localScale.z);
            controller.Move(Vector3.up * deltaHeight);
        }

        // Jumping
        if (gameOptions.jump.GetKey() && playerData.isOnGround) {
            playerData.isOnGround = false;
            playerData.velocity.y = jumpSpeed / 50;
        }
        
        // Gravity
        playerData.velocity.y -= fallSpeed / 50;
        controller.Move(playerData.velocity);

        // Calculate rotation from mouse movement
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity * gameOptions.mouseSensitivity.Value;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity * gameOptions.mouseSensitivity.Value;
        playerData.yRot += mouseX;
        playerData.xRot = Mathf.Clamp(playerData.xRot - mouseY, -88, 88);

        // Rotate the player along only the horizontal axis
        transform.rotation = Quaternion.Euler(0, playerData.yRot, 0);

        // Rotate the player's look vector along both axes (the camera inherits this transformation)
        playerLook.rotation = Quaternion.Euler(playerData.xRot, playerData.yRot, 0);
        
        // Apply view bobbing if the player is moving horizontally
        if (playerData.isOnGround && horizontalVelocity.magnitude > 0) {
            Vector3 viewBobbing = playerData.playerState switch {
                PlayerState.Crouching => ViewBobbing(bobbingSpeedCrouching, bobbingIntensityCrouching),
                PlayerState.Sprinting => ViewBobbing(bobbingSpeedSprinting, bobbingIntensitySprinting),
                _ => ViewBobbing(bobbingSpeed, bobbingIntensity)
            };
            // Add the actual vector to the Player Look object's position
            playerLook.localPosition += viewBobbing;
        }

        // Calculate focal point
        Vector3 origin = playerLook.position - playerLook.localPosition + initialPlayerLook;
        // RaycastHit hit;
        // if (Physics.Raycast(origin, playerLook.forward, out hit, focalDistance)) cameraFocus = hit.point;
        /*else */cameraFocus = origin + playerLook.forward * focalDistance;

        // Compensate for bobbing offset
        playerLook.LookAt(cameraFocus, Vector3.up);
    }

    void Update() {
        // Don't do anything if the game isn't playing
        if (GameManager.instance.gameState != GameManager.GameState.Playing) return;

        // Slowly reset the camera's position after bobbing
        if (playerLook.localPosition != initialPlayerLook) {
            playerLook.localPosition = Vector3.Lerp(playerLook.localPosition, initialPlayerLook, Time.deltaTime * 3);
        }
    }

    private Vector3 ViewBobbing(float freq, float amp) {
        // Calculate view bobbing offset
        Vector3 vec = Vector3.zero;
        vec.x += (Mathf.Cos(Time.time * freq / 2f)) * amp * 1.2f;
        vec.y += (Mathf.Sin(Time.time * freq)) * amp;
        return vec;
    }

    /// <summary>
    /// Returns a normalized vector of where the player is trying to move.
    /// </summary>
    private Vector3 GetInputVector() {
        float forward, sideways;
        // Front/back movement
        if (gameOptions.forward.GetKey()) forward = 1;
        else forward = gameOptions.back.GetKey() ? -1 : 0;
        // Side-to-side movement
        if (gameOptions.right.GetKey()) sideways = 1;
        else sideways = gameOptions.left.GetKey() ? -1 : 0;
        return (transform.forward * forward + transform.right * sideways).normalized;
    }

    private float MoveSpeed() {
        return moveSpeed * playerData.playerState switch {
            PlayerState.Crouching => crouchModifier,
            PlayerState.Sprinting => sprintModifier,
            _ => 1
        };
    }

    [Serializable]
    public enum PlayerState {
        Normal, Crouching, Sprinting
    }

}
