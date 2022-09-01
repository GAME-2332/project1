using System;
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

    /// <summary>
    /// Represents a generic game option field. Really just a prettier wrapper class for a PlayerPrefs
    /// field to minimize code duplication. PlayerPrefs caches values, so we don't need to maintain
    /// our own value cache within the Field class.
    /// </summary>
    /// <typeparam name="T">The field's type.</typeparam>
    public abstract class Field<T> {
        private readonly string key;
        private readonly T defaultValue;

        public Field(string key, T defaultValue) {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// The field's value as cached from PlayerPrefs.
        /// </summary>
        public T Value {
            get => GetPref(key, defaultValue);
            set => SetPref(key, value);
        }

        // Get the current value from PlayerPrefs
        protected abstract T GetPref(string key, T fallback);

        // Set the current value within PlayerPrefs
        protected abstract void SetPref(string key, T value);
    }

    /// <summary>
    /// Integer field implementation.
    /// </summary>
    public class IntField: Field<int> {
        public IntField(string key, int defaultValue) : base(key, defaultValue) {}
        
        protected override int GetPref(string key, int fallback) {
            return PlayerPrefs.GetInt(key, fallback);
        }
        
        protected override void SetPref(string key, int value) {
            PlayerPrefs.SetInt(key, value);
        }
    }

    /// <summary>
    /// Float field implementation.
    /// </summary>
    public class FloatField: Field<float> {
        public FloatField(string key, float defaultValue) : base(key, defaultValue) {}
        
        protected override float GetPref(string key, float fallback) {
            return PlayerPrefs.GetFloat(key, fallback);
        }
        
        protected override void SetPref(string key, float value) {
            PlayerPrefs.SetFloat(key, value);
        }
    }

    /// <summary>
    /// String field implementation.
    /// </summary>
    public class StringField: Field<string> {
        public StringField(string key, string defaultValue) : base(key, defaultValue) {}
        
        protected override string GetPref(string key, string fallback) {
            return PlayerPrefs.GetString(key, fallback);
        }
        
        protected override void SetPref(string key, string value) {
            PlayerPrefs.SetString(key, value);
        }
    }

    public class BooleanField: Field<bool> {
        public BooleanField(string key, bool defaultValue) : base(key, defaultValue) {}

        protected override bool GetPref(string key, bool fallback) {
            return PlayerPrefs.GetInt(key, fallback ? 1 : 0) == 1;
        }

        protected override void SetPref(string key, bool value) {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }
    }

    /// <summary>
    /// Enum field implementation.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    // This one is significantly more janky but anything to reduce code duplication man.
    // We store the Type of the generic (I know, I know) and cache the value separately here
    // to avoid parsing unecessarily often.
    //
    // I'm used to Java's type erasure so the actual capability to do this is a breath of
    // fresh air
    //
    // I paid for the whole runtime and I'll damn well use the whole runtime
    public class EnumField<T>: Field<T> where T: Enum {
        private readonly Type enumType;
        private T cache;

        public EnumField(string key, T defaultValue) : base(key, defaultValue) {
            enumType = typeof(T);
            cache = defaultValue;
        }

        protected override T GetPref(string key, T fallback) {
            string current = PlayerPrefs.GetString(key, fallback.ToString());
            if (cache.ToString().Equals(current)) return cache;
            return (T) Enum.Parse(enumType, current);
        }

        protected override void SetPref(string key, T value) {
            PlayerPrefs.SetString(key, value.ToString());
        }
    }

    public class KeyCodeField: EnumField<KeyCode> {
        public KeyCodeField(string key, KeyCode defaultValue) : base(key, defaultValue) {}
    }
}
