using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsQuitButton : MonoBehaviour
{
    [SerializeField]
    Button _quitbutton;
    // Start is called before the first frame update
    void Start()
    {
        if(_quitbutton == null)
        {
            _quitbutton = GetComponentInChildren<Button>();
        }
        _quitbutton.onClick.AddListener(OnClickQuit);
    }

    void OnClickQuit()
    {
        GameManager.instance.gameOptions.Save();
    }

}
