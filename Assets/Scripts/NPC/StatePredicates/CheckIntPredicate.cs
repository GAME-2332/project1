using System;
using System.ComponentModel;

[Serializable]
public class CheckIntPredicate : CheckGlobalsPredicate<int> {
    [DefaultValue(Mode.Equal)]
    public Mode mode;
    
    public override int GetValue(string name) => GameManager.instance.saveState.globals.GetInt(name);

    public override bool Check() {
        int check = GetValue(variableName);
        return mode switch {
            Mode.NotEqual => check != checkValue,
            Mode.GreaterThan => check > checkValue,
            Mode.GreaterThanOrEqual => check >= checkValue,
            Mode.LessThan => check < checkValue,
            Mode.LessThanOrEqual => check <= checkValue,
            _ => check == checkValue
        };
    }

    public enum Mode {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }
}