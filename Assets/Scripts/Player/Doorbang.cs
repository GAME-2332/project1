using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorbang : MonoBehaviour
{
    public AudioSource BangBang;
    void Start(){
        BangBang.GetComponent<AudioSource>().enabled = false;
    }

    private void OnTriggerEnter(){
        BangBang.GetComponent<AudioSource>().enabled = true;
    }
    private void OnTriggerExit(){
        BangBang.GetComponent<AudioSource>().enabled = false;

    }
}
