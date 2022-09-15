using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI_Components
{
   public class SaveSelector : MonoBehaviour
    {
        List<SaveBox> saves;
        private void OnEnable()
        {
            MainButtons.OnLoadGameClicked += OnClickOpenCanvas;
            MainButtons.OnNewGameClicked += OnClickOpenCanvas;
        }
        private void OnDiable()
        {
            MainButtons.OnLoadGameClicked -= OnClickOpenCanvas;
            MainButtons.OnNewGameClicked -= OnClickOpenCanvas;
        }
        [SerializeField]
        Button _BackgroundButton;
        [SerializeField]
        CanvasGroup _cg;
        // Start is called before the first frame update
        void Start()
        {
            OnClickCloseCanvas();
            if (_BackgroundButton == null)
            {
                _BackgroundButton = transform.GetChild(0).GetComponent<Button>(); 
            }
            _BackgroundButton.onClick.AddListener(OnClickCloseCanvas);

            if(_cg == null)
            {
                _cg = GetComponent<CanvasGroup>();
            }

            SaveBox[] sb = GetComponentsInChildren<SaveBox>();
            int i = 0;
            foreach(SaveBox s in sb)
            {
                i++;
                s.SetSlotID(i);
            }
        }

        public void OnClickCloseCanvas()
        {
            _cg.alpha = 0;
            _cg.blocksRaycasts = false;
            _cg.interactable = false;
        }

        public void OnClickOpenCanvas()
        {
            _cg.alpha = 1;
            _cg.blocksRaycasts = true;
            _cg.interactable = true;
        }

    }
}
