using UnityEngine;

/// <summary>
/// Boolean field implementation.
/// </summary>
public class BooleanField : Field<bool> {
    public BooleanField(string key, bool defaultValue) : base(key, defaultValue) { }

    protected override bool GetPref(string key, bool fallback) {
        return PlayerPrefs.GetInt(key, fallback ? 1 : 0) == 1;
    }

    protected override void SetPref(string key, bool value) {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
}