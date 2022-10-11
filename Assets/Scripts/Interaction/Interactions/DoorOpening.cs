using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour, IRuntimeSerialized
{
    public float Speed;
    public Vector3 endPosition;
    private Vector3 startPosition;
    [SerializeField]
    [HideInInspector]
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

    public void OpenDoor(){
        isOpen = true;
    }
}
