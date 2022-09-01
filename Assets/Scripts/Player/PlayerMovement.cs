using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A component providing basic movement controls for the player based on configured inputs from the GameManager.
/// </summary>
public class PlayerMovement : MonoBehaviour {
    public Transform playerLook;
    public float bobbingIntensity;
    public float bobbingSpeed;
    public float bobbingIntensitySprinting;
    public float bobbingSpeedSprinting;
    public float bobbingIntensityCrouching;
    public float bobbingSpeedCrouching;
    public float mouseSensitivity;
    public float moveSpeed;
    public float sprintModifier;
    public float crouchModifier;
    public float crouchHeight;
    public float jumpForce;

    private GameOptions gameOptions;
    new private Rigidbody rigidbody;
    private Vector3 playerLookZero;
    private Vector3 crouchingScale;
    private float xRot = 0;
    private float yRot = 0;
    private PlayerState playerState = PlayerState.Normal;
    private float groundDistance;
    private bool isOnGround = true;

    void Start() {
        gameOptions = GameManager.instance.gameOptions;
        rigidbody = GetComponent<Rigidbody>();
        playerLookZero = playerLook.localPosition;
        crouchingScale = new Vector3(1, crouchHeight, 1);
        groundDistance = GetComponent<Collider>().bounds.extents.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate() {
        // Calculate movement in FixedUpdate, per physics tick
        // First, check if we're on the ground
        isOnGround = Physics.Raycast(transform.position, Vector3.down, groundDistance * transform.localScale.y + .1f);

        // Check if sprinting/crouching
        // TODO: Stamina
        bool canSprint = isOnGround;
        if (Input.GetKey(gameOptions.crouch.Value)) {
            playerState = PlayerState.Crouching;
        } else {
            if (canSprint && Input.GetKey(gameOptions.sprint.Value)) playerState = PlayerState.Sprinting;
            else playerState = PlayerState.Normal;
        }

        // Apply player movement force
        rigidbody.AddForce(GetInputVector() * MoveSpeed(), ForceMode.Force);
        // Max speed to avoid crazy acceleration with AddForce weirdness
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, 5f);

        // Crouching
        if (playerState == PlayerState.Crouching) {
            if (!transform.localScale.Equals(crouchingScale)) {
                transform.localScale = Vector3.Lerp(transform.localScale, crouchingScale, .7f);
                rigidbody.AddForce(Vector3.down * moveSpeed, ForceMode.Force);
            }
        } else transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, .3f);

        // Jumping
        if (Input.GetKey(gameOptions.jump.Value) && isOnGround) {
            isOnGround = false;
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        // Apply view bobbing if the player is moving fast enough horizontally
        float groundSpeed = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).magnitude;
        if (isOnGround && groundSpeed > .5f) {
            Vector3 viewBobbing = playerState switch {
                PlayerState.Crouching => ViewBobbing(bobbingSpeedCrouching, bobbingIntensityCrouching),
                PlayerState.Sprinting => ViewBobbing(bobbingSpeedSprinting, bobbingIntensitySprinting),
                _ => ViewBobbing(bobbingSpeed, bobbingIntensity)
            };
            // Add the actual vector to the Player Look object's position
            playerLook.localPosition += viewBobbing;
        }
    }

    void Update() {
        // Calculate rotation/looking around in Update, every frame
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity * gameOptions.mouseSensitivity.Value;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity * gameOptions.mouseSensitivity.Value;

        yRot += mouseX;
        xRot = Mathf.Clamp(xRot - mouseY, -90, 90);

        // Rotate the player along only the horizontal axis
        transform.rotation = Quaternion.Euler(0, yRot, 0);
        // Rotate the player's look vector along both axes (the camera inherits this transformation)
        playerLook.rotation = Quaternion.Euler(xRot, yRot, 0);

        // Slowly reset the camera's position after bobbing
        if (playerLook.localPosition != playerLookZero) {
            playerLook.localPosition = Vector3.Lerp(playerLook.localPosition, playerLookZero, Time.deltaTime * 3);
        }
    }

    private Vector3 ViewBobbing(float freq, float amp) {
        // Calculate view bobbing offset
        Vector3 vec = Vector3.zero;
        vec.x += (Mathf.Cos(Time.time * freq / 2f)) * amp * 1.3f;
        vec.y += (Mathf.Sin(Time.time * freq)) * amp;
        return vec;
    }

    /// <summary>
    /// Returns a normalized vector of where the player is trying to move.
    /// </summary>
    private Vector3 GetInputVector() {
        float forward, sideways;
        if (GameManager.instance.usingJoystickControls) {
            // TODO: If we end up using joystick controls, we need to use only
            // a specific joystick's input axes based on the control menu
            forward = Input.GetAxis("Vertical");
            sideways = Input.GetAxis("Horizontal");
        } else {
            // Using mouse and keyboard
            // Front/back movement
            if (Input.GetKey(gameOptions.forward.Value)) forward = 1;
            else forward = (Input.GetKey(gameOptions.back.Value) ? -1 : 0);
            // Side-to-side movement
            if (Input.GetKey(gameOptions.right.Value)) sideways = 1;
            else sideways = (Input.GetKey(gameOptions.left.Value) ? -1 : 0);
        }
        return (transform.forward * forward + transform.right * sideways).normalized;
    }

    private float MoveSpeed() {
        return moveSpeed * playerState switch {
            PlayerState.Crouching => crouchModifier,
            PlayerState.Sprinting => sprintModifier,
            _ => 1
        };
    }

    public enum PlayerState {
        Normal, Crouching, Sprinting
    }

}
