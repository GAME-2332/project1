using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    List<SOItemInfo> _Inventory;
    List<GameObject> _Spaces;

    Canvas _InventoryCanvas;
    CanvasGroup _InventoryCanvasGroup;

    [SerializeField]
    Button BackButton;

    [SerializeField]
    SOItemInfo DEV_TEST_ITEM;

    static bool AlreadyExists;

    public static bool GetExists()
    {
        return AlreadyExists;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(AlreadyExists == true)
        {
            Destroy(this.gameObject);
        }
        else
        {
            AlreadyExists = true;
        }
         _InventoryCanvas = GetComponent<Canvas>();
        _InventoryCanvasGroup = GetComponent<CanvasGroup>();
        _InventoryCanvasGroup.alpha = 0;
        StartCoroutine("OpenCanvas");

        LoadInventory();
        if (BackButton == null)
        {
            BackButton = GameObject.Find("Back Button").GetComponent<Button>();
        }
        BackButton.onClick.AddListener(OnClickExit);
    }
    float increment = 0.05f;
    IEnumerator OpenCanvas()
    {
        while (_InventoryCanvasGroup.alpha < 1)
        {
            _InventoryCanvasGroup.alpha += increment;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator CloseCanvas()
    {
        AlreadyExists = false;
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();

        while (_InventoryCanvasGroup.alpha > 0)
        {
            _InventoryCanvasGroup.alpha -= increment;
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void  OnClickExit()
    {
        GameManager.instance.gameState = GameManager.GameState.Playing;
        StartCoroutine("CloseCanvas");
    }

    public void LoadInventory()
    {
        //_Inventory = GameManager.instance.saveState.GetInventory()
        LoadSpaces();//this gets a reference to the spaces.

        //Inventory is now a list of itemSO's.
        for (int i = 0; i < GameManager.instance.saveState.inventory.Count; i++) {
            _Spaces[i].GetComponentInChildren<ItemInfo>().SetSO(GameManager.instance.saveState.inventory.GetItem(i));
        }
    }

    void LoadSpaces()
    {
        _Spaces = new List<GameObject>();
        GameObject _inventorySpace = GameObject.Find("Inventory Space");
        for (int i = 0; i < _inventorySpace.transform.childCount; i++)
        {
            _Spaces.Add(_inventorySpace.transform.GetChild(i).gameObject);
        }
        Debug.Log("Successfully found spaces.");



        //_Spaces[0].GetComponentInChildren<ItemInfo>().SetSO(DEV_TEST_ITEM);



        return;
    }
}
