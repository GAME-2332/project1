using UnityEngine;

/// <summary>
/// The NPC component stores information about an NPC and handles opening or creating the DialogueScreen instance.
/// </summary>
public class NPC: Interactible {
    
    public string npcName;
    public DialogueTree dialogueTree;

    private bool shouldOpen = false;
    
    public override void Interact() {
        DialogueScreen.GetOrCreate();
        shouldOpen = true;
    }

    void LateUpdate() {
        if (shouldOpen) DialogueScreen.GetOrCreate().Open(npcName, dialogueTree);
        shouldOpen = false;
    }
}