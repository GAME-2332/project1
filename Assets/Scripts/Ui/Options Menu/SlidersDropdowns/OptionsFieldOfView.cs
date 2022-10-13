using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsFieldOfView : MonoBehaviour
{
    [SerializeField]
    Slider _slider;
    [SerializeField]
    ButtonInput _display;

    bool IsStarting = true;
    // Start is called before the first frame update
    void Start()
    {
        IsStarting = true;
        GetReferences();
        ReadValue();
    }

    void GetReferences()
    {
        if(_slider == null)
        {
            _slider = GetComponentInChildren<Slider>();
        }
        _slider.onValueChanged.AddListener(ChangedValue);
        if(_display == null)
        {
            _display = GetComponentInChildren<ButtonInput>();
        }
    }
    [SerializeField]
    int value;
    void ReadValue()//used at Start
    {
        value = GameManager.instance.gameOptions.fieldOfView.Value; //value will be between 30 to 120, default at 60
        _display.SetText(value.ToString());
        float f = (float)value;
        f -=  30;
        f = f / 90f;
        _slider.value = f;
        _display.SetText(value.ToString());
    }


    void ChangedValue(float f)
    {
        if(IsStarting == true) //do  not call changed value if you are starting, or else you'll end up adding 30 back in!
        {
            IsStarting = false;
        }
        else
        {
            if (f > 0.28f && f < 0.38f)
            {
                f = 0.333333f;
                _slider.value = f;
            }

            //f will be between 0 and 1. We should convert to beween 30 and 120. range of 90. 
            //multiply by 90
            //add 30.
            f = f * 90;
            f += 30;
            value = Mathf.RoundToInt(f);
            GameManager.instance.gameOptions.fieldOfView.Value = value;
            _display.SetText(value.ToString());
        }
        
    }

}
