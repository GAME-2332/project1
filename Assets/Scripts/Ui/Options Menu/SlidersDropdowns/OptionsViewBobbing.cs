using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsViewBobbing : MonoBehaviour
{
    [SerializeField]
    Toggle _toggle;
    // Start is called before the first frame update
    void Start()
    {
        if(_toggle == null)
        {
            _toggle = GetComponentInChildren<Toggle>();
        }
        _toggle.onValueChanged.AddListener(OnValueChanged);

        _toggle.isOn = GameManager.instance.gameOptions.viewBobbing.Value;
    }

    void OnValueChanged(bool b)
    {
        GameManager.instance.gameOptions.viewBobbing.Value = b;
    }

}
