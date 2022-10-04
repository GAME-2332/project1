using System;
using UnityEngine;

/// <summary>
/// Stores global fields of a few types that are serialized and deserialized within a save slot.
/// </summary>
[Serializable]
public class Globals {
    [SerializeField]
    private SerializedMap<string, int> intMap = new SerializedMap<string, int>();
    [SerializeField]
    private SerializedMap<string, float> floatMap = new SerializedMap<string, float>();
    [SerializeField]
    private SerializedMap<string, bool> boolMap = new SerializedMap<string, bool>();
    [SerializeField]
    private SerializedMap<string, string> stringMap = new SerializedMap<string, string>();

    public void SetInt(string key, int value) {
        intMap.Set(key, value);
    }

    public int GetInt(string key, int? fallback = null) {
        return intMap.GetOrDefault(key, fallback ?? 0);
    }

    public void SetFloat(string key, float value) {
        floatMap.Set(key, value);
    }

    public float GetFloat(string key, float? fallback = null) {
        return floatMap.GetOrDefault(key, fallback ?? 0f);
    }

    public void SetBool(string key, bool value) {
        boolMap.Set(key, value);
    }

    public bool GetBool(string key, bool? fallback = null) {
        return boolMap.GetOrDefault(key, fallback ?? false);
    }

    public void SetString(string key, string value) {
        stringMap.Set(key, value);
    }

    public string GetString(string key, string fallback = null) {
        return stringMap.GetOrDefault(key, fallback ?? "");
    }
}