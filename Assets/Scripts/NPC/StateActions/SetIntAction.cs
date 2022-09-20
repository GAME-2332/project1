using System;

[Serializable]
public class SetIntAction : SetGlobalsAction<int> {
    public override void Execute() {
        GameManager.instance.saveState.globals.SetInt(variableName, value);
    }
}