using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MainMenuUI_Components
{
    public class TimeText : MonoBehaviour
    {
        TMPro.TMP_Text _timetext;
        // Start is called before the first frame update
        void Start()
        {
            if (_timetext == null)
            {
                _timetext = GetComponentInChildren<TMPro.TMP_Text>();
            }
        }
        public void SetText(string s)
        {
            if (_timetext == null)
            {
                _timetext = GetComponentInChildren<TMPro.TMP_Text>();
            }
            _timetext.text = s;
        }
    }

}