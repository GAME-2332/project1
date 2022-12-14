using System;

[Serializable]
public class CheckBoolPredicate : CheckGlobalsPredicate<bool> {
    public override bool GetValue(string name) => GameManager.instance.saveState.globals.GetBool(name);
}