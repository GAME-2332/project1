using System;

[Serializable]
public class BoolPredicate : GlobalsPredicate<bool> {
    public override bool GetValue(string name) => GameManager.instance.saveState.globals.GetBool(name);
}