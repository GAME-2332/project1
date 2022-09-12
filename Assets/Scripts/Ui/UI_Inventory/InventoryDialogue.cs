using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDialogue : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text TMPName;
    [SerializeField]
    TMPro.TMP_Text TMPDescription;
    // Start is called before the first frame update
    void Start()
    {
        LoadReferences();
        SetTMPName();
        SetTMPDescription();
    }

    void LoadReferences()
    {
        if (TMPName == null)
        {
            TMPName = GameObject.Find("Item_Title_Space").transform.GetChild(0).GetComponent<TMP_Text>();
        }
        if (TMPDescription == null)
        {
            TMPDescription = GameObject.Find("Item_Description_Space").transform.GetChild(0).GetComponent<TMP_Text>();
        }
    }
    // Update is called once per frame
   public void SetTMPName(string s = "")
    {
        if (TMPName == null)
        {
            TMPName = GameObject.Find("Item_Title_Space").transform.GetChild(0).GetComponent<TMP_Text>();
        }
        TMPName.text = s;
    }
    public void SetTMPDescription(string s = "")
    {
        if (TMPDescription == null)
        {
            TMPDescription = GameObject.Find("Item_Description_Space").transform.GetChild(0).GetComponent<TMP_Text>();
        }
        TMPDescription.text = s;
    }
}
