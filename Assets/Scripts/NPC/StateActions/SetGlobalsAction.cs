using System;

/// <summary>
/// Sets a given variable in the current save state's Globals.
/// </summary>
[Serializable]
public abstract class SetGlobalsAction<T> : IStateAction {
    public string variableName;
    public T value;

    public abstract void Execute();
}