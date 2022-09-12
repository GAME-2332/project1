using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button_Tester : MonoBehaviour
{
    Button MyButton;
    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClickMyButton);
    }

    void OnClickMyButton()
    {
        Debug.Log("Button was pressed.");
    }

}
