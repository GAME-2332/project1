using System;

[Serializable]
public class StringPredicate : GlobalsPredicate<string> {
    public override string GetValue(string name) => GameManager.instance.saveState.globals.GetString(name);
}