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

        UnlockCursor();
        LoadInventory();
        if (BackButton == null)
        {
            BackButton = GameObject.Find("Back Button").GetComponent<Button>();
        }
        BackButton.onClick.AddListener(OnClickExit);
    }
    float increment = 0.2f;
    IEnumerator OpenCanvas()
    {
      
        while (_InventoryCanvasGroup.alpha < 1)
        {
            _InventoryCanvasGroup.alpha += increment;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator CloseCanvas()
    {
       
        while (_InventoryCanvasGroup.alpha > 0)
        {
            _InventoryCanvasGroup.alpha -= increment;
            yield return new WaitForSeconds(0.1f);
        }
        AlreadyExists = false;
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();
    }
    public void  OnClickExit()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine("CloseCanvas");
    }

    public void LoadInventory()
    {
        _Inventory = new List<SOItemInfo>();
        

        //Get the game manager, get the list of game objects. 
        //_Inventory = GameManager.instance.saveState.GetInventory()
        LoadSpaces();//this gets a reference to the spaces.

        int itemCount = 0;



        //New foreach loader 
        //Inventory is now a list of itemSO's.
        foreach (SOItemInfo i in _Inventory)
        {
            _Spaces[itemCount].GetComponentInChildren<ItemInfo>().SetSO(i);
            itemCount++;
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


    private void Update()
    {
        UnlockCursor();
    }
    public void UnlockCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
}
