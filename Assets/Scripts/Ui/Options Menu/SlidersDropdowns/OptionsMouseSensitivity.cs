using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMouseSensitivity : MonoBehaviour
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
        if (_slider == null)
        {
            _slider = GetComponentInChildren<Slider>();
        }
        _slider.onValueChanged.AddListener(ChangedValue);
        if (_display == null)
        {
            _display = GetComponentInChildren<ButtonInput>();
        }
    }
    [SerializeField]
    int value;
    void ReadValue()//used at Start
    {
        _slider.value = GameManager.instance.gameOptions.mouseSensitivity.Value;
        value = Mathf.RoundToInt( GameManager.instance.gameOptions.mouseSensitivity.Value  * 100); //multiply by 100 to get percentage.

        string s = value.ToString("##.") + "%";
        if(value < 1)
        {
            s = "0%";
        }

        _display.SetText(s);

    }


    void ChangedValue(float f)
    {
        if (IsStarting == true) //do  not call changed value if you are starting, or else you'll end up adding 30 back in!
        {
            IsStarting = false;
        }
        else
        {

            if(f < 0.55f && f > 0.45f)
            {
                f = 0.5f;
                _slider.value = f;
            }
            
            value = Mathf.RoundToInt(f * 100);
            GameManager.instance.gameOptions.mouseSensitivity.Value = f;

            string s = value.ToString("##.") + "%";
            if (value < 1)
            {
                s = "0%";
            }


            _display.SetText(s);
        }

    }
}
