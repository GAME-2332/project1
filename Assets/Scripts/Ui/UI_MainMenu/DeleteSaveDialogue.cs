using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI_Components
{
    public class DeleteSaveDialogue : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup _canvasgroup;

        [SerializeField]
        Button _delete;
        [SerializeField]
        Button _cancel;

        [SerializeField]
        Button _background;
        private void OnEnable()
        {
            DeleteButton.OnDeleteClickedEvent += OpenCanvas;
        }

        private void OnDisable()
        {
            DeleteButton.OnDeleteClickedEvent -= OpenCanvas;
        }
        // Start is called before the first frame update
        void Start()
        {

            if(_canvasgroup == null)
            {
                _canvasgroup = GetComponent<CanvasGroup>();
            }

            if(_canvasgroup.alpha != 0)
            {
                CloseCanvas();
            }

            if(_background == null)
            {
                _background = transform.GetChild(0).GetComponent<Button>();
            }
            _background.onClick.AddListener(CloseCanvas);

            Transform buttons = transform.GetChild(0).GetChild(1);
            if(_delete == null)
            {
                _delete= buttons.GetChild(0).GetComponent<Button>();
            }
            _delete.onClick.AddListener(OnDelete);

            if(_cancel == null)
            {
                _cancel = buttons.GetChild(1).GetComponent<Button>();
            }
            _cancel.onClick.AddListener(OnCancel);
        }

        public void OnDelete()
        {
            CloseCanvas();
        }
        public void OnCancel()
        {
            CloseCanvas();
        }
        public void OpenCanvas()
        {
            _canvasgroup.blocksRaycasts = true;
            _canvasgroup.alpha = 1;
        }
        public void CloseCanvas()
        {
            _canvasgroup.alpha = 0;
            _canvasgroup.blocksRaycasts = false;
        }
    }
}

