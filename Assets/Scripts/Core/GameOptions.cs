using UnityEngine;

/// <summary>
/// A class containing game options and keybindings serialized via PlayerPrefs.
/// </summary>
public class GameOptions {
    // Misc options
    public readonly IntField fieldOfView = new IntField("Options.ViewField", 60);
    public readonly FloatField mouseSensitivity = new FloatField("Options.SensitivityX", .5f);
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
}
