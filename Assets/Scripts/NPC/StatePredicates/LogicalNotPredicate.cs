using System;
using UnityEngine;

/// <summary>
/// Predicate that inverts the check of the contained predicate.
/// </summary>
[Serializable]
public class LogicalNotPredicate : IStatePredicate {
    [PickImpl(typeof(IStatePredicate))]
    [SerializeReference] public IStatePredicate predicate;

    public bool Check() {
        return !predicate.Check();
    }
}