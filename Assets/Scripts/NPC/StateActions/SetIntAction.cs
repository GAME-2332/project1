using System;
using System.ComponentModel;

[Serializable]
public class SetIntAction : SetGlobalsAction<int> {
    [DefaultValue(Mode.Set)]
    public Mode mode;
    
    public override void Execute() {
        GameManager.instance.saveState.globals.SetInt(variableName, mode switch {
            Mode.Add => GameManager.instance.saveState.globals.GetInt(variableName) + value,
            Mode.Subtract => GameManager.instance.saveState.globals.GetInt(variableName) - value,
            _ => value
        });
    }

    public enum Mode {
        Set, Add, Subtract
    }
}