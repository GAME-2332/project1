using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.EventSystems;
using System.Data;

namespace MainMenuUI_Components
{
    

    public class DeleteButton : MonoBehaviour
    {
        public delegate void OpenDeleteDialogue();
        public static event OpenDeleteDialogue OnDeleteClickedEvent;

        Image _icon;
        [SerializeField]
        Sprite _IconOnHover;
        [SerializeField]
        Sprite _IconDefault;

        [SerializeField]
        Button _deleteButton;
        [SerializeField]
        public Texture2D cursorTextureClickable;
        EventTrigger _et;
        // Start is called before the first frame update
        void Start()
        {
            if(_deleteButton == null)
            {
                _deleteButton = GetComponentInChildren<Button>();
            }
            _deleteButton.onClick.AddListener(OnDeleteWasClicked);
           
            _icon = transform.GetChild(0).GetComponent<Image>();
            if (cursorTextureClickable == null)
            {
                cursorTextureClickable = Resources.Load("UI/cursor_clickable") as Texture2D;
            }
            if(_IconOnHover == null)
            {
                _IconOnHover = Resources.Load<Sprite>("UI/MainMenu/Trashicon_withghost");
            }
            if (_IconDefault == null)
            {
                _IconDefault = Resources.Load<Sprite>("UI/MainMenu/Trashicon_withoutghost");
            }
            _et = this.gameObject.AddComponent<EventTrigger>();

            Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerEnter;

            _et.triggers.Add(entry);

            entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });

            Entry entry2 = new EventTrigger.Entry();

            _et.triggers.Add(entry2);

            entry2.eventID = EventTriggerType.PointerExit;

            entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });

        }

        public void OnDeleteWasClicked()
        {
            OnDeleteClickedEvent();
        }
        public void OnPointerEnterDelegate(PointerEventData data)
        {
            
            _icon.sprite = _IconOnHover;
        }

        public void OnPointerExitDelegate(PointerEventData data)
        {
            _icon.color = Color.white;
            _icon.sprite = _IconDefault;
        }
    }
}
