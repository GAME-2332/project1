using System;

[Serializable]
public class SetStringAction : SetGlobalsAction<string> {
    public override void Execute() {
        GameManager.instance.saveState.globals.SetString(variableName, value);
    }
}