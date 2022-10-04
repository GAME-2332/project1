using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : Interactible
{
    public Light Light;
    public AudioClip flickOnSound;
    public AudioClip flickOffSound;
    private AudioSource LightAudio;

    void Start(){
        Light.enabled = false;
        LightAudio = GetComponent<AudioSource>();
    }

    public override void Interact() {
        Light.enabled = !Light.enabled;
        LightAudio.clip = Light.enabled ? flickOnSound : flickOffSound;
        LightAudio.Play();
    }

}
