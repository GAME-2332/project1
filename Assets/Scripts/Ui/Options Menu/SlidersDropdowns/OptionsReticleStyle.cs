using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsReticleStyle : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Dropdown _dropdown;

    List<GameOptions.ReticleStyle> _reticleStyles;

    List<string> _modesTitle;
    // Start is called before the first frame update
    void Start()
    {
        if (_dropdown == null)
        {
            _dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();

        }
        //GameManager.instance.gameOptions.windowMode;

        _reticleStyles = new List<GameOptions.ReticleStyle>();  
        _modesTitle = new List<string>();

        int current_value = 0;
        int i = 0;

        foreach(GameOptions.ReticleStyle R in System.Enum.GetValues(typeof(GameOptions.ReticleStyle)))
        {
            _reticleStyles.Add(R);
            _modesTitle.Add(R.ToString());

            if(R == GameManager.instance.gameOptions.reticleStyle.Value)
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
        _dropdown.captionText.text = GameManager.instance.gameOptions.reticleStyle.Value.ToString();

    }

    void ChangeInput(int i)
    {
        GameManager.instance.gameOptions.reticleStyle.Value = _reticleStyles[i];
       
    }
}
