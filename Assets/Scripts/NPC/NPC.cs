using UnityEngine;

/// <summary>
/// The NPC component stores information about an NPC and handles opening or creating the DialogueScreen instance.
/// </summary>
public class NPC: Interactible {
    
    public string npcName;
    public DialogueTree dialogueTree;
    
    public override void Interact() {
        DialogueScreen.GetOrCreate().Open(npcName, dialogueTree);
    }
}