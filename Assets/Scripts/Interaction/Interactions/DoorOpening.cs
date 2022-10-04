using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : Interactible
{
    public float Speed;
    public Vector3 endPosition;
    private Vector3 startPosition;
    private bool isOpen = false;

    void Start(){
        startPosition = transform.position;
        endPosition += startPosition;
    }

    void Update(){
        if(isOpen){
            transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, endPosition) <= 1f) this.enabled = false;
        }
    }

    public override void Interact(){
        if(Input.GetKey(KeyCode.E)){
            // Door.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + 3f);
            isOpen = true;
        }
    }

    public override void SetOutline(bool enabled) {
        base.SetOutline(enabled && !isOpen);
    }
}
