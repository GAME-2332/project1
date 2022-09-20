using System;

[Serializable]
public class IntPredicate : GlobalsPredicate<int> {
    public override int GetValue(string name) => GameManager.instance.saveState.globals.GetInt(name);
}