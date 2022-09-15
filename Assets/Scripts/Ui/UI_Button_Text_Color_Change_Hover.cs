using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;

public class UI_Button_Text_Color_Change_Hover : MonoBehaviour
{
    //Attach this to the BUTTON (so hover can work) of any text you want to have a color change on hover!

    EventTrigger _et;
    [SerializeField]
    TMPro.TMP_Text _text;

    //static Color hoverColor = new Color(207f / 256f,206f / 256f, 94f / 256f);
    static Color hoverColor = Color.red;
    static Color normalColor = new Color(25f / 256f, 131f / 256f, 28f / 256f);

    [SerializeField]
    GameObject _selectedImage;
    [SerializeField]
    public Texture2D cursorTextureClickable;
    // Start is called before the first frame update
    void Start()
    {
        if(_selectedImage==null)
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

        if(_text == null)
        {
            _text = GetComponentInChildren<TMPro.TMP_Text>();
        }
       
    }

    GameObject _selectedFrame;

    public void OnPointerEnterDelegate(PointerEventData data)
    {
      
        _text.color = hoverColor;
        _selectedFrame = Instantiate(_selectedImage, transform);
        _selectedFrame.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        Cursor.SetCursor(cursorTextureClickable, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        Destroy(_selectedFrame);
        _text.color = normalColor;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


}
