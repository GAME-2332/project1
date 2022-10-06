using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    Button _exitButton;
    // Start is called before the first frame update
    void Start()
    {
        if(_exitButton == null)
        {
            _exitButton = GameObject.FindObjectOfType<OptionsQuitButton>().GetComponent<Button>();
        }
        _exitButton.onClick.AddListener(QuitOptionsMenu);
    }

    void QuitOptionsMenu()
    {
        Object.Destroy(this.gameObject);
    }
}
