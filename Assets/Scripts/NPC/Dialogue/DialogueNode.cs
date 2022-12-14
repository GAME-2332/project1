using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Describes a single point in a dialogue thread.
/// Can be used to represent a single line of dialogue, or a choice.<br />
/// Can be displayed conditionally with IStatePredicates, and run arbitrary code with IStateActions.
/// </summary>
[Serializable]
public class DialogueNode {
    [Tooltip("The text to show in an option box for this node, if it's a child of another.")]
    public string optionText;
    [Tooltip("If present, the portrait to switch to when this dialogue node is selected.")]
    public Sprite portrait;
    [Tooltip("If true, dialogue will terminate when this node's NPC text is completed.")]
    public bool isTerminator;
    [Tooltip("The text the NPC will say when this node is selected. Each element requires the continue key to display after the previous one.")]
    public DialogueLine[] npcText;
    
    [Tooltip("Conditions for this node to appear as an option in dialogue.")]
    [SerializeReference] [PickImpl(typeof(IStatePredicate))]
    public List<IStatePredicate> conditions;
    [Tooltip("Actions to run after this node's NPC text is completed.")]
    [SerializeReference] [PickImpl(typeof(IStateAction))]
    public List<IStateAction> actions;
    
    [Tooltip("Child nodes to display as options after the NPC is done speaking.")]
    public List<DialogueNode> children;

    /// <summary>
    /// Gets all this node's children that currently meet their display conditions.
    /// </summary>
    public IEnumerable<DialogueNode> GetValidChildren() {
        return children.Where(s => s.IsValid());
    }
    
    /// <summary>
    /// Executes all this node's actions.
    /// </summary>
    public void ExecuteActions() {
        actions.ForEach(s => s.Execute());
    }

    /// <summary>
    /// Checks if all this node's conditions are met.
    /// </summary>
    private bool IsValid() {
        if (conditions == null) return true;
        foreach (var condition in conditions) {
            if (!condition.Check()) return false;
        }
        return true;
    }

    [Serializable]
    public struct DialogueLine {
        [TextArea(1, 10)]
        public string text;
        [Tooltip("If nonnull, the portrait will be replaced with this sprite when this line is spoken.")]
        [SerializeReference] public Sprite portrait;
    }
}