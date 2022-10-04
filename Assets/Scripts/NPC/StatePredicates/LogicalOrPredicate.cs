using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Predicate that returns true if any of its contained predicates are true.
/// </summary>
[Serializable]
public class LogicalOrPredicate : IStatePredicate {
    [PickImpl(typeof(IStatePredicate))]
    [SerializeReference] public List<IStatePredicate> predicates;

    public bool Check() {
        return predicates.Any(s => s.Check());
    }
}