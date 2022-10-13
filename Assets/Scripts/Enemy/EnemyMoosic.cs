using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyMoosic : MonoBehaviour
{
    [NonSerialized]
    public AudioSource AudioChase;
    void Start(){
        AudioChase =  GetComponent<AudioSource>();
        AudioChase.enabled = false;
    }

    public void AudioPlay(){
        if (AudioChase.GetComponent<AudioSource>().isPlaying == false){
            AudioChase.enabled = true;
            FindObjectOfType<PlayerCamera>().backgroundMusic.Pause();
        }
    }
    public void AudioStop(){
        if (AudioChase.GetComponent<AudioSource>().enabled == true){
            FindObjectOfType<PlayerCamera>().backgroundMusic.Play();
            AudioChase.GetComponent<AudioSource>().enabled = false;
        }
    }
}
