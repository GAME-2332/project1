using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

namespace MainMenuUI_Components
{


    public class KeyBinder_Graphics : MonoBehaviour
    {
        public delegate void ACTIVATELISTEN(string key_id);
        public static event ACTIVATELISTEN STARTACTIVATELISTEN;

        [SerializeField]
        GameObject _selectedImage;
        [SerializeField]
        public Texture2D cursorTextureClickable;

        [SerializeField]
        string my_key_id;

        [SerializeField]
        Button _button;

        EventTrigger _et;

        bool listening_for_input;

        // Start is called before the first frame update
        void Start()
        {
            GetReferences();
            SetUpEventTrigger();
        }
        private void OnEnable()
        {
            KeyBinder.OnSetListeningState += SetListeningState;
        }
        private void OnDisable()
        {

            KeyBinder.OnSetListeningState -= SetListeningState;
        }
        void GetReferences()
        {
            if (_selectedImage == null)
            {
                _selectedImage = Resources.Load("UI/Image_Button_Selected") as GameObject;
            }
            if(_button == null)
            {
                _button = GetComponent<Button>();
            }
            _button.onClick.AddListener(OnClickButton);
            if (cursorTextureClickable == null)
            {
                cursorTextureClickable = Resources.Load("UI/cursor_clickable") as Texture2D;
            }
            my_key_id = transform.GetChild(0).GetComponentInChildren<TMPro.TMP_Text>().text;
            
        }

        void SetUpEventTrigger()
        {
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

        public void SetListeningState(string s = "")
        {
            if(s == "stop graphics because we are waiting for input")
            {
                listening_for_input = true;
                _button.enabled = false;
            }
            else if(s == "start graphics interactions")
            {
                StartCoroutine("StartInteractionsAgain");
                if (_selectedFrame != null)
                {
                    Destroy(_selectedFrame);
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    //if it is already selected [clicked on], leave the frame there.
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
            else
            {

            }
        }
        void OnClickButton()
        {
            
            STARTACTIVATELISTEN(my_key_id);
        }

        IEnumerator StartInteractionsAgain()
        {
            yield return new WaitForSeconds(0.3f);
            listening_for_input = false;
        }
        public string GetMyKeyID()
        {
            return my_key_id;
        }

        GameObject _selectedFrame;

        public void OnPointerEnterDelegate(PointerEventData data)
        {
            if (listening_for_input == true)
            {
                //do nothing
            }
            else
            {
                _selectedFrame = Instantiate(_selectedImage, transform);
                Cursor.SetCursor(cursorTextureClickable, Vector2.zero, CursorMode.Auto);
                _button.enabled = true;
            }

        }

        public void OnPointerExitDelegate(PointerEventData data = null)
        {
            if(listening_for_input == false)
            {
                if (_selectedFrame != null)
                {
                    Destroy(_selectedFrame);
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    //if it is already selected [clicked on], leave the frame there.
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
            

        }

    }

}