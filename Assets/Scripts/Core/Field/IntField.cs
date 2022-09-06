using UnityEngine;

/// <summary>
/// Integer field implementation.
/// </summary>
public class IntField : Field<int> {
    public IntField(string key, int defaultValue) : base(key, defaultValue) { }

    protected override int GetPref(string key, int fallback) {
        return PlayerPrefs.GetInt(key, fallback);
    }

    protected override void SetPref(string key, int value) {
        PlayerPrefs.SetInt(key, value);
    }
}