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