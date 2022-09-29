using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject Light;

    void Start(){
        Light.GetComponent<Light>().enabled = false;
    }

    private void TurnOn(){
        if(Input.GetKey(KeyCode.E)){
            Light.GetComponent<Light>().enabled = true;
        }
  }
}
