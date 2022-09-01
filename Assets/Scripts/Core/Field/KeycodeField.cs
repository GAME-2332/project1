using UnityEngine;

/// <summary>
/// Keycode field implementation, with helper methods to check current input values.
/// </summary>
public class KeyCodeField : EnumField<KeyCode> {
    public KeyCodeField(string key, KeyCode defaultValue) : base(key, defaultValue) { }

    /// <summary>
    /// Checks whether the key is currently held down.
    /// </summary>
    public bool GetKey() {
        return Input.GetKey(Value);
    }

    /// <summary>
    /// Checks whether the key was initially pressed down this frame.
    /// </summary>
    public bool GetKeyDown() {
        return Input.GetKeyDown(Value);
    }

    /// <summary>
    /// Checks whether the key was released this frame.
    /// </summary>
    public bool GetKeyUp() {
        return Input.GetKeyUp(Value);
    }
}