using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Blocker : MonoBehaviour
{
    [SerializeField]
    Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("destroying");
        Destroy(this);
    }
}
