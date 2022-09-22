using System;

[Serializable]
public class CheckStringPredicate : CheckGlobalsPredicate<string> {
    public override string GetValue(string name) => GameManager.instance.saveState.globals.GetString(name);
}