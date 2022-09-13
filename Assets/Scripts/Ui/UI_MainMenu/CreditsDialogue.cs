using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;

namespace MainMenuUI_Components
{

    public class CreditsDialogue : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup _cg;
        [SerializeField]
        Button _all;

        private void OnEnable()
        {
            CreditsButton.OpenCreditsEvent += OpenCanvas;
              
        }

        private void OnDisable()
        {
            CreditsButton.OpenCreditsEvent -= OpenCanvas;
        }
        // Start is called before the first frame update
        void Start()
        {
            CloseCanvas();
            _cg = GetComponent<CanvasGroup>();
            if (_cg == null)
            {
                _cg = GetComponent<CanvasGroup>();
            }
            if(_all == null)
            {
                _all = GetComponentInChildren<Button>();
            }
            _all.onClick.AddListener(CloseCanvas) ;

        }


        public void CloseCanvas()
        {
            if (_cg == null)
            {
                _cg = GetComponent<CanvasGroup>();
            }
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
            _cg.alpha = 0;
        }
        public void OpenCanvas()
        {
            Debug.Log("opening canvas!");
            _cg.interactable = true;
            _cg.blocksRaycasts = true;
            _cg.alpha = 1;
        }
    }
}
