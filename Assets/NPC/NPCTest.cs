using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCTest : Interactible {
    public GameObject spritePrefab;
    public TextMesh npcName;
    public TextMesh npcText;
    public TextMesh[] options;
    public DialogueTree dialogueTree;
    int yOffset = 0;
    int xValue = 0;
    int yValue = 0;

    public void Redraw(string npcText, string[] options) {
        
        
    }

   public void Interact() {
        
    }
}
