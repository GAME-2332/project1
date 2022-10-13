using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedAction : IStateAction {
    [SerializeReference] [PickImpl(typeof(IStateAction))]
    public IStateAction action;
    public int delayMillis;

    public void Execute() {
        SceneData.Get(SceneManager.GetActiveScene()).Schedule(action, delayMillis);
    }
}