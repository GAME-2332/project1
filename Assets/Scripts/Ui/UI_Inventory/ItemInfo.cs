using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string item_name;

    [SerializeField]
    string item_description;

    [SerializeField]
    SOItemInfo item_so_info;

    InventoryDialogue _inventorydialogue;
    private void Start()
    {
        //Find the item dialogue so that we may populate it later.
        _inventorydialogue = GameObject.FindObjectOfType<InventoryDialogue>();
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
