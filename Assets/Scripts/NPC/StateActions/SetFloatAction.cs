using System;

[Serializable]
public class SetFloatAction : SetGlobalsAction<float> {
    public override void Execute() {
        GameManager.instance.saveState.globals.SetFloat(variableName, value);
    }
}