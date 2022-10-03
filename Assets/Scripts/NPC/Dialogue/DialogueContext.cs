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
    private DialogueNode[] validChildren;
    
    private Action<string> refreshNpcText;
    private Action<string[]> refreshOptions;
    private Action<Sprite> changePortrait;
    private Action onDialogueEnd;
    
    public string NpcText {
        get => currentNode.npcText.Length < 1 ? "..." : currentNode.npcText[npcTextIndex];
    }
    
    /// <summary>
    /// Creates a new DialogueContext with a given entry point, ignoring its conditions.
    /// </summary>
    /// <param name="tree">The dialogue tree</param>
    /// <param name="refreshOptions">A function to run when dialogue options have changed - note the passed array may be null or empty</param>
    /// <param name="onDialogueEnd">A function to run when dialogue has ended; used to close the UI</param>
    /// 
    internal DialogueContext(DialogueTree tree, Action<string> refreshNpcText, Action<string[]> refreshOptions, Action<Sprite> changePortrait, Action onDialogueEnd) {
        this.refreshNpcText = refreshNpcText;
        this.refreshOptions = refreshOptions;
        this.changePortrait = changePortrait;
        this.onDialogueEnd = onDialogueEnd;
        
        currentNode = tree.entryPoint;
        changePortrait.Invoke(tree.portrait);
    }

    public void Ready() {
        NextNode(currentNode);
    }

    /// <summary>
    /// If the NPC is speaking, and there are still lines to say, sets the DialogueBoxText to the next line.<br />
    /// If the NPC is done speaking, and choices are already displayed to the player, does nothing.
    /// </summary>
    public void Continue() {
        // Increment the NPC's line index; if there are no more lines, set the dialogue options
        if (NpcSpeaking()) {
            npcTextIndex++;
            refreshNpcText.Invoke(NpcText);
            if (npcTextIndex == currentNode.npcText.Length - 1) LastLine();
        }
        else if (currentNode.isTerminator) {
            onDialogueEnd();
        }
    }

    /// <summary>
    /// If the NPC is speaking and there are still lines to say, or if the passed choice index is invalid, does nothing.<br />
    /// Otherwise, chooses the given dialogue option, sets all text fields to represent the new node, and runs the new nodes actions.
    /// </summary>
    public void Choose(int index) {
        if (NpcSpeaking() || index < 0 || index >= validChildren.Length) return;
        NextNode(validChildren[index]);
    }
    
    /// <summary>
    /// Sets the current node and all relevant fields
    /// </summary>
    /// <param name="nextNode"></param>
    private void NextNode(DialogueNode nextNode) {
        currentNode = nextNode;
        validChildren = currentNode.GetValidChildren().ToArray();
        npcTextIndex = 0;
        refreshNpcText.Invoke(NpcText);
        refreshOptions(null);
        if (currentNode.portrait != null) changePortrait(currentNode.portrait);
        if (currentNode.npcText.Length <= 1) LastLine();
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