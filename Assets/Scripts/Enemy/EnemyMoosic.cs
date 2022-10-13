using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyMoosic : MonoBehaviour
{
    public AudioSource AudioChase;
    public AudioSource AudioChase2;
    
    void Start(){
        AudioChase.enabled = false;
    }

    public void AudioPlay(){
        if (AudioChase.GetComponent<AudioSource>().isPlaying == false){
            AudioChase.enabled = true;
            AudioChase2.enabled = true;
            FindObjectOfType<PlayerCamera>().backgroundMusic.Pause();
        }
    }
    public void AudioStop(){
        if (AudioChase.GetComponent<AudioSource>().enabled == true){
            FindObjectOfType<PlayerCamera>().backgroundMusic.Play();
            AudioChase.enabled = false;
            AudioChase2.enabled = false;
        }
    }
}
