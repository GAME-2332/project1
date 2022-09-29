using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : Interactible
{
    public GameObject Door;
    public Transform transform;
    
    private bool isOpen;

    public override void Interact(){
        if(Input.GetKey(KeyCode.E) && !isOpen){
            Door.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + 3f);
            isOpen = true;
        }
    }

    public override void SetOutline(bool enabled) {
        base.SetOutline(enabled && !isOpen);
    }
}
