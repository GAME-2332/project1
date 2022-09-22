using UnityEngine;
 using System.Collections;
 
 public class MenuToggle : MonoBehaviour {
     private Canvas CanvasObject; // Assign in inspector
      
     void Start(){
         CanvasObject = GetComponent<Canvas> ();
     }
 
     void Update(){
         if (Input.GetKeyUp(KeyCode.E)) {
            GameManager.instance.gameState = CanvasObject.enabled ? GameManager.GameState.Playing : GameManager.GameState.Dialogue;
            CanvasObject.enabled = !CanvasObject.enabled;
         }
     }
 }
