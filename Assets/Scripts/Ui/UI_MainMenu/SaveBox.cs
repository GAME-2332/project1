using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.EventSystems;
using System.Data;
using UnityEngine.SceneManagement;

namespace MainMenuUI_Components
{

    public class SaveBox : MonoBehaviour
    {
        [SerializeField]
        public int SLOT_ID;
        // Start is called before the first frame update
        [SerializeField]
        Image _background;
        [SerializeField]
        Button _mainButton;
        EventTrigger _et;

        [SerializeField]
        GameObject _deleteButton;

        LocationText _locationText;
        TimeText _timeText;
       
        [SerializeField]
        GameObject _selectedImage;
        [SerializeField]
        public Texture2D cursorTextureClickable;


        private SaveState saveState;

        [SerializeField]


        // Start is called before the first frame update
        void Start()
        {

            saveState = new SaveState(SLOT_ID);
            
            if (_locationText == null)
            {
                _locationText = GetComponentInChildren<LocationText>();
            }
            if(_timeText == null)
            {
                _timeText = GetComponentInChildren<TimeText>();
            }
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

            if (saveState.Exists())
            {
                _mainButton.onClick.AddListener(OnClickLoadSavedGame);
                _locationText.SetText(saveState.lastSceneName);
                _timeText.SetText(saveState.lastSaveDate + " " + saveState.lastSaveTime);
                //saveState.Load();
            }
            else
            {
                _mainButton.onClick.AddListener(OnClickLoadNewGame);
                _locationText.SetText("EMPTY SLOT");
                _timeText.SetText("----");
                //load with "empty slot!"
            }


        }
        
        public void OnClickLoadNewGame()
        {
            GameManager.instance.saveState = saveState;

            saveState.Load(); 
            Debug.Log("starting new game");
            // SceneManager.LoadScene("IntroScene");
        }
        public void OnClickLoadSavedGame()
        {
            GameManager.instance.saveState = saveState;
            saveState.Load();
            Debug.Log("loading game");
        }
        /*
        GameManager.instance.saveState = saveState;
        saveState.Load();
        */
        public void SetSlotID(int ID)
        {
            SLOT_ID = ID;

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
