using UnityEngine;

//// <summary>
/// String field implementation.
/// </summary>
public class StringField : Field<string> {
    public StringField(string key, string defaultValue) : base(key, defaultValue) { }

    protected override string GetPref(string key, string fallback) {
        return PlayerPrefs.GetString(key, fallback);
    }

    protected override void SetPref(string key, string value) {
        PlayerPrefs.SetString(key, value);
    }
}