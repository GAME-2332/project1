/// <summary>
/// Defines a void action for dialogue or quest events to modify the global game state.
/// </summary>
public interface IStateAction {
    public void Execute();
}