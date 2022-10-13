using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour, IRuntimeSerialized
{
    public float Speed;
    public Vector3 endPosition;
    private Vector3 modEndPosition;
    private Vector3 startPosition;
    [SerializeField]
    [HideInInspector]
    private bool isOpen = false;

    void Start(){
        startPosition = transform.position;
        modEndPosition = endPosition + startPosition;
    }

    void Update(){
        if(isOpen){
            transform.position = Vector3.Lerp(transform.position, modEndPosition, Time.deltaTime * Speed);
            if (Vector3.Distance(transform.position, modEndPosition) <= 1f) this.enabled = false;
        }
    }

    public void OpenDoor(){
        isOpen = true;
    }
}
