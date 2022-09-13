using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI_Components {



    public class CreditsButton : MonoBehaviour
    {
        public delegate void CreditsAction();
        public static event CreditsAction OpenCreditsEvent;

        [SerializeField]
        Button _button;
        // Start is called before the first frame update
        void Start()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
            _button.onClick.AddListener(OnClickCredits);
            
        }

        void OnClickCredits()
        {
            OpenCreditsEvent();
        }
    }


}

