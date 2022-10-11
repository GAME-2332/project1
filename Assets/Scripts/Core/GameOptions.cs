
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

/// <summary>
/// A class containing game options and keybindings serialized via PlayerPrefs.
/// </summary>
public class GameOptions {
    // Misc options
    public readonly IntField fieldOfView = new IntField("Options.ViewField", 60); // 30 - 120
    public readonly FloatField mouseSensitivity = new FloatField("Options.SensitivityX", .5f); // 0 - 1
    public readonly BooleanField viewBobbing = new BooleanField("Options.ViewBobbing", true);
    public readonly EnumField<ReticleStyle> reticleStyle = new EnumField<ReticleStyle>("Options.ReticleStyle", ReticleStyle.Dot);
    public readonly EnumField<WindowMode> windowMode = new EnumField<WindowMode>("Options.WindowMode", WindowMode.Windowed);

    // Keybinds
    public readonly KeyCodeField forward = new KeyCodeField("Controls.Keyboard.Forward", KeyCode.W);
    public readonly KeyCodeField back = new KeyCodeField("Controls.Keyboard.Back", KeyCode.S);
    public readonly KeyCodeField left = new KeyCodeField("Controls.Keyboard.Left", KeyCode.A);
    public readonly KeyCodeField right = new KeyCodeField("Controls.Keyboard.Right", KeyCode.D);
    public readonly KeyCodeField jump = new KeyCodeField("Controls.Keyboard.Jump", KeyCode.Space);
    public readonly KeyCodeField sprint = new KeyCodeField("Controls.Keyboard.Sprint", KeyCode.LeftShift);
    public readonly KeyCodeField crouch = new KeyCodeField("Controls.Keyboard.Crouch", KeyCode.LeftControl);
    public readonly KeyCodeField interact = new KeyCodeField("Controls.Keyboard.Interact", KeyCode.E);
    public readonly KeyCodeField inventory = new KeyCodeField("Controls.Keyboard.Inventory", KeyCode.Tab);

    // Forces serialization of all fields.
    public void Save() {
        PlayerPrefs.Save();
        GameManager.instance.events.optionsReloadEvent.Invoke();
    }

    public enum ReticleStyle {
        None, Dot, Circle
    }

    public enum WindowMode {
        Windowed, Borderless, Fullscreen
    }
    public void SetKey(string keyname, KeyCode keyvalue)
    {
        if(keyname == "FORWARD")
        {
            Debug.Log("updating Forward");
            forward.Value = keyvalue;
            
        }
        if (keyname == "LEFT")
        {
            Debug.Log("updating Left");
            left.Value = keyvalue;
        }
        if (keyname == "RIGHT")
        {
            Debug.Log("updating Right");
            right.Value = keyvalue;
        }
        if (keyname == "BACK")
        {
            Debug.Log("updating Back");
            back.Value = keyvalue;
        }
        if (keyname == "JUMP")
        {
            Debug.Log("updating Jump");
            jump.Value = keyvalue;
        }
        if(keyname == "SPRINT")
        {
            Debug.Log("updating Sprint");
            sprint.Value = keyvalue;
        }
        if (keyname == "CROUCH")
        {
            Debug.Log("updating Crouch");
            crouch.Value = keyvalue;
        }
        if (keyname == "INTERACT")
        {
            Debug.Log("updating Interact");
            interact.Value = keyvalue;
        }

    }
}
