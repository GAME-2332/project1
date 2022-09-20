using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine TMPro;

public class NPC : Interactible {
    public GameObject spritePrefab;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcText;
    public TextMeshProUGUI[] options;
    public DialogueTree dialogueTree;
    int yOffset = 0;
    int xValue = 0;
    int yValue = 0;

    public void Redraw(string npcText, string[] options) {
        
        
    }

    public override void Interact() {
        
    }
}
