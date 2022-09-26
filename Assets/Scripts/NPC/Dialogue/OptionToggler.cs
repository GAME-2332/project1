using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionToggler : MenuToggle
{
    public Canvas CanvasOption;
    public GameObject CanvasListOption;
    void Start(){
         CanvasOption = GetComponent<Canvas>();
     }
   
    public void OpenCanvas(){
        //CanvasOption.enabled = !CanvasOption.enabled;
        if (CanvasListOption.activeInHierarchy == true){
            CanvasListOption.SetActive(false);
        }else{
            CanvasListOption.SetActive(true);
        }
        
    }
}
