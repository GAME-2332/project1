using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectInteraction : Interactible{
    [Tooltip("Conditions for this interaction.")]
    [SerializeReference] [PickImpl(typeof(IStatePredicate))]
    public List<IStatePredicate> conditions;
    [Tooltip("Actions to run upon interaction if the conditions are met.")]
    [SerializeReference] [PickImpl(typeof(IStateAction))]
    public List<IStateAction> actions;
    [Tooltip("Actions to run upon interaction if the conditions are not met.")]
    [SerializeReference] [PickImpl(typeof(IStateAction))]
    public List<IStateAction> elseActions;

    [FormerlySerializedAs("disableIfSuccesful")] public bool disableIfSuccessful;
    
    public override void Interact() {
        if (conditions.Count(predicate => predicate.Check()) == conditions.Count) {
            actions.ForEach(action => action.Execute());
            if (disableIfSuccessful) {
                gameObject.SetActive(false);
            }
        } else {
            elseActions.ForEach(action => action.Execute());
        }
    }
}
