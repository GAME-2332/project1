using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource AudioTrigger;

    void Start(){
        AudioTrigger = GetComponent<AudioSource>();
        AudioTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player") AudioTrigger.GetComponent<AudioSource>().enabled = true;
    }

    private void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "Player") AudioTrigger.GetComponent<AudioSource>().enabled = false;
    }
}
