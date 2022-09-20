using System;

[Serializable]
public class SetBoolAction : SetGlobalsAction<bool> {
    public override void Execute() {
        GameManager.instance.saveState.globals.SetBool(variableName, value);
    }
}