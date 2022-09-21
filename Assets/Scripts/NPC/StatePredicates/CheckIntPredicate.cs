using System;

[Serializable]
public class CheckIntPredicate : CheckGlobalsPredicate<int> {
    public override int GetValue(string name) => GameManager.instance.saveState.globals.GetInt(name);
}