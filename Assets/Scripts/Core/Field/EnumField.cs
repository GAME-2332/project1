using UnityEngine;
using System;

/// <summary>
/// Enum field implementation.
/// </summary>
/// <typeparam name="T">The enum type.</typeparam>
//
// I paid for the whole runtime and I'll damn well use the whole runtime
public class EnumField<T> : Field<T> where T : Enum {
    private readonly Type enumType;
    private T cache;

    public EnumField(string key, T defaultValue) : base(key, defaultValue) {
        enumType = typeof(T);
        cache = defaultValue;
    }

    protected override T GetPref(string key, T fallback) {
        string current = PlayerPrefs.GetString(key, fallback.ToString());
        if (cache.ToString().Equals(current)) return cache;
        return (T)Enum.Parse(enumType, current);
    }

    protected override void SetPref(string key, T value) {
        PlayerPrefs.SetString(key, value.ToString());
    }
}