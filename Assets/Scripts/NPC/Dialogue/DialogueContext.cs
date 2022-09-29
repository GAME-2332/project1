using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used for dialogue tree traversal.
/// Has methods to conveniently fetch which text should be displayed and which options are available.
/// </summary>
public class DialogueContext {
    private int npcTextIndex;
    private DialogueNode currentNode;
    
    private Action<string[]> refreshOptions;
    private Action<Sprite> changePortrait;
    private Action onDialogueEnd;
    
    public string NpcText {
        get => currentNode.npcText.Length < 1 ? "..." : currentNode.npcText[npcTextIndex];
    }
    
    /// <summary>
    /// Creates a new DialogueContext with a given entry point, ignoring its conditions.
    /// </summary>
    /// <param name="startNode">The entry point of the dialogue tree</param>
    /// <param name="refreshOptions">A function to run when dialogue options have changed - note the passed array may be null or empty</param>
    /// <param name="onDialogueEnd">A function to run when dialogue has ended; used to close the UI</param>
    /// 
    internal DialogueContext(DialogueNode startNode, Action<string[]> refreshOptions, Action<Sprite> changePortrait, Action onDialogueEnd) {
        NextNode(startNode);
        this.refreshOptions = refreshOptions;
        this.changePortrait = changePortrait;
        this.onDialogueEnd = onDialogueEnd;
    }

    /// <summary>
    /// If the NPC is speaking, and there are still lines to say, sets the DialogueBoxText to the next line.<br />
    /// If the NPC is done speaking, and choices are already displayed to the player, does nothing.
    /// </summary>
    public void Continue() {
        // Increment the NPC's line index; if there are no more lines, set the dialogue options
        if (NpcSpeaking())
            if (++npcTextIndex == currentNode.npcText.Length - 1) LastLine();
        else if (currentNode.isTerminator) {
            if (onDialogueEnd != null) onDialogueEnd();
        }
    }

    /// <summary>
    /// If the NPC is speaking and there are still lines to say, or if the passed choice index is invalid, does nothing.<br />
    /// Otherwise, chooses the given dialogue option, sets all text fields to represent the new node, and runs the new nodes actions.
    /// </summary>
    public void Choose(int index) {
        if (NpcSpeaking() || index < 0 || index >= currentNode.children.Count) return;
        NextNode(currentNode.children[index]);
    }
    
    /// <summary>
    /// Sets the current node and all relevant fields
    /// </summary>
    /// <param name="nextNode"></param>
    private void NextNode(DialogueNode nextNode) {
        currentNode = nextNode;
        npcTextIndex = 0;
        if (currentNode.portrait != null) changePortrait(currentNode.portrait);
        if (currentNode.npcText.Length <= 1) LastLine();
        else refreshOptions(null);
    }
    
    /// <summary>
    /// Checks whether the NPC text is still in progress
    /// </summary>
    private bool NpcSpeaking() => npcTextIndex < currentNode.npcText.Length - 1;

    /// <summary>
    /// Sets up dialogue options and runs the node's actions; called after the NPC is done talking (duh doy)
    /// </summary>
    private void LastLine() {
        currentNode.ExecuteActions();
        if (!currentNode.isTerminator) {
            refreshOptions(currentNode.GetValidChildren().Select(node => node.optionText).ToArray());
        }
    }
}