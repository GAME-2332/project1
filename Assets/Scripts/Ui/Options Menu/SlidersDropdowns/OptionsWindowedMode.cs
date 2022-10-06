using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindowedMode : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Dropdown _dropdown;
    List<GameOptions.WindowMode> _windowModes;
    List<string> _modesTitle;
    // Start is called before the first frame update
    void Start()
    {
        if(_dropdown == null)
        {
            _dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
           
        }
        //GameManager.instance.gameOptions.windowMode;
        _windowModes = new List<GameOptions.WindowMode>();
        _modesTitle = new List<string>();

        int current_value = 0;
        int i = 0;
        foreach( GameOptions.WindowMode W in System.Enum.GetValues(typeof(GameOptions.WindowMode)))
        {
            _windowModes.Add(W);
            _modesTitle.Add(W.ToString());
            
            if (W == GameManager.instance.gameOptions.windowMode.Value)
            {
                current_value = i;
            }
            i++;
        }
        _dropdown.ClearOptions();
        _dropdown.AddOptions(_modesTitle);
        _dropdown.onValueChanged.AddListener(ChangeInput);
        _dropdown.value = current_value;
        
    }
    private void Update()
    {
        _dropdown.captionText.text = GameManager.instance.gameOptions.windowMode.Value.ToString();

    }

    void ChangeInput(int i)
    {
        GameManager.instance.gameOptions.windowMode.Value = _windowModes[i];
    }

}
