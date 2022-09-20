using System;

[Serializable]
public class FloatPredicate : GlobalsPredicate<float> {
    public override float GetValue(string name) => GameManager.instance.saveState.globals.GetFloat(name);
}