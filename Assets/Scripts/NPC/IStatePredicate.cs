/// <summary>
/// Describes a predicate function for dialogue and quest events to check the current game state.
/// </summary>
public interface IStatePredicate {
    public bool Check();
}