using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class ItemInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string item_name;

    [SerializeField]
    string item_description;

    [SerializeField]
    SOItemInfo item_so_info;

    [SerializeField]
    EventTrigger _et;

    InventoryDialogue _inventorydialogue;

    Image _sprite;

    public ItemInfo(SOItemInfo _so)
    {
        item_so_info = _so;
        item_name = _so.item_name;
        item_description = _so.item_description;

    }
    private void Start()
    {
        if(_et == null)
        {
            _et = GetComponent<EventTrigger>(); 
        }
        SetUpEvent();
        //Find the item dialogue so that we may populate it later.
        _inventorydialogue = GameObject.FindObjectOfType<InventoryDialogue>();
    }

    void SetUpEvent()
    {
        Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerEnter;

        _et.triggers.Add(entry);

        entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });

        Entry entry2 = new EventTrigger.Entry();

        _et.triggers.Add(entry2);

        entry2.eventID = EventTriggerType.PointerExit;

        entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
    }

    public void SetSO(SOItemInfo _so)
    {
        item_so_info = _so;
    }


    public void OnPointerEnterDelegate(PointerEventData data)
    {

        OnHover();
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        OnHoverOver();
    }
    public void OnHover()
    {
        _inventorydialogue.SetTMPName(item_name);
        _inventorydialogue.SetTMPDescription(item_description);
    }

    public void OnHoverOver()
    {
        _inventorydialogue.SetTMPName("");
        _inventorydialogue.SetTMPDescription("");
    }
}
