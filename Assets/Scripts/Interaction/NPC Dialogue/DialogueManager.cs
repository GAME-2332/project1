using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

    float distance;
    float currentResponseTracker = 0; //Which response is needed to generate in respone to player dialogue

    public GameObject player;
    public GameObject dialogueUI;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogueBox;
    public TextMeshProUGUI playerResponse;




    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void OnMouseOver(){
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if(distance <= 2.5f){

            if(Input.GetAxis("Mouse ScrollWheel") < 0f){
                currentResponseTracker++;
                if(currentResponseTracker >= npc.playerDialogue.Length - 1){

                    currentResponseTracker = npc.playerDialogue.Length - 1;

                }

            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0f){

                currentResponseTracker--;
                if(currentResponseTracker < 0){
                    currentResponseTracker = 0; //This makes it so you dont leave the bounce of a conversation
                }
            }


            //trigger dialogue
            if(Input.GetKeyDown(KeyCode.E) && isTalking==false ){
                
                StartConversation();

            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking == true){

                EndDialogue();
            }

            if(currentResponseTracker == 0 && npc.playerDialogue.Length > 0){
                playerResponse.text = npc.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Return)){
                    npcDialogueBox.text = npc.dialogue[1];
                }

            }
            else if(currentResponseTracker == 1 && npc.playerDialogue.Length >= 1){
                playerResponse.text = npc.playerDialogue[1];
                if(Input.GetKeyDown(KeyCode.Space)){
                    npcDialogueBox.text = npc.dialogue[2];
                }
            } //Have many responses as needed, but for each time you want to add, this else if block must be created
            else if(currentResponseTracker == 2 && npc.playerDialogue.Length >= 2){
                playerResponse.text = npc.playerDialogue[2];
                if(Input.GetKeyDown(KeyCode.Space)){
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
        }
    }

    void StartConversation(){
        isTalking = true;
        currentResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];

    }


    void EndDialogue(){
        isTalking = false;
        dialogueUI.SetActive(false);


    }
}
