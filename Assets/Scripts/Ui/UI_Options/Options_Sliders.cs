using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options_Sliders : MonoBehaviour
{

    Slider _FieldOfView;
    TMPro.TMP_Text _FieldOfView_TextValue;
    float _fieldOfViewValue;

    // Start is called before the first frame update
    void Start()
    {
        _FieldOfView = transform.GetChild(0).GetComponentInChildren<Slider>();
        _FieldOfView.onValueChanged.AddListener(FieldOfViewValueChanged);
    }

    void FieldOfViewValueChanged(float f)
    {
        Debug.Log("new value:" + f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
