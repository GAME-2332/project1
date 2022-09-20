using System;

[Serializable]
public class CheckFloatPredicate : CheckGlobalsPredicate<float> {
    public override float GetValue(string name) => GameManager.instance.saveState.globals.GetFloat(name);
}