using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    //This class is used to easily find a Text that needs to change dynamically.
    [SerializeField]
    TMPro.TMP_Text _text;
    private void Start()
    {
        if(_text == null)
        {
            _text = GetComponentInChildren<TMPro.TMP_Text>();
        }
    }
    public void SetText(string s)
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<TMPro.TMP_Text>();
        }
        _text.text = s;
    }
}
