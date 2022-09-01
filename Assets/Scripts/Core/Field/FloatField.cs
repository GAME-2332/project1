using UnityEngine;

/// <summary>
/// Float field implementation.
/// </summary>
public class FloatField : Field<float> {
    public FloatField(string key, float defaultValue) : base(key, defaultValue) { }

    protected override float GetPref(string key, float fallback) {
        return PlayerPrefs.GetFloat(key, fallback);
    }

    protected override void SetPref(string key, float value) {
        PlayerPrefs.SetFloat(key, value);
    }
}