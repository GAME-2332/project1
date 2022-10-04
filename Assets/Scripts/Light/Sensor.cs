using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public GameObject Light;
    public AudioSource LightSensor;

    void Start(){
        Light.GetComponent<Light>().enabled = false;
        LightSensor.GetComponent<AudioSource>().enabled = false;
    }

    private void OnTriggerEnter(Collider other){
        Light.GetComponent<Light>().enabled = true;
        LightSensor.enabled = true;
  }
  
    private void OnTriggerExit(Collider other){
        Light.GetComponent<Light>().enabled = false;
        LightSensor.enabled = false;

    }
}
