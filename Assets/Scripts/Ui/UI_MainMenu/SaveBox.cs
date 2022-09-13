using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.EventSystems;
using System.Data;

namespace MainMenuUI_Components
{

    public class SaveBox : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        Image _background;
        [SerializeField]
        Button _mainButton;
        EventTrigger _et;

        [SerializeField]
        GameObject _deleteButton;

        [SerializeField]
        GameObject _selectedImage;
        [SerializeField]
        public Texture2D cursorTextureClickable;
        // Start is called before the first frame update
        void Start()
        {
            if (_mainButton == null)
            {
                _mainButton = GetComponentInChildren<Button>();  
            }
            if (_deleteButton == null)
            {
                _deleteButton = Resources.Load("UI/MainMenu/DeleteButtonPrefab") as GameObject;
            }


            if (_background == null)
            {
                _background = GetComponent<Image>();
            }
            if (_selectedImage == null)
            {
                _selectedImage = Resources.Load("UI/Image_Button_Selected") as GameObject;
            }
            if (cursorTextureClickable == null)
            {
                cursorTextureClickable = Resources.Load("UI/cursor_clickable") as Texture2D;
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

        GameObject _selectedFrame;
        GameObject _deleteInstance;
        public void OnPointerEnterDelegate(PointerEventData data)
        {

            _background.color = new Color(0f,0f,0f,0.5f);
            
            _selectedFrame = Instantiate(_selectedImage, transform);
            _selectedFrame.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            _deleteInstance = Instantiate(_deleteButton, transform);
            _deleteInstance.transform.SetSiblingIndex(transform.GetSiblingIndex() + 2);
            Cursor.SetCursor(cursorTextureClickable, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerExitDelegate(PointerEventData data)
        {
            Destroy(_selectedFrame);
            Destroy(_deleteInstance);
            _background.color = new Color(0f, 0f, 0f, 0f);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
