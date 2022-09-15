using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MainMenuUI_Components {
    
    public class LocationText : MonoBehaviour
    {


        [SerializeField]
        TMPro.TMP_Text _locationtext;
        // Start is called before the first frame update
        void Start()
        {
            if(_locationtext == null)
            {
                _locationtext = GetComponentInChildren<TMPro.TMP_Text>(); 
            }
        }

        public void SetText(string s)
        {
            if (_locationtext == null)
            {
                _locationtext = GetComponentInChildren<TMPro.TMP_Text>();
            }
            _locationtext.text = s;   
        }
    }


}
