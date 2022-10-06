using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyMoosic : MonoBehaviour
{
    private AudioSource AudioChase;
    void Start(){
        AudioChase = GetComponent<AudioSource>();
        AudioChase.enabled = false;
    }

    public void AudioPlay(){
        AudioChase.GetComponent<AudioSource>().enabled = true;
        
    }
    public void AudioStop(){
        AudioChase.GetComponent<AudioSource>().enabled = false;
        
    }
}
