using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    List<GameObject> _Inventory;
    List<GameObject> _Spaces;

    [SerializeField]
    GameObject DevTestItem;
    [SerializeField]
    GameObject DevTestItem2;

    // Start is called before the first frame update
    void Start()
    {
        UnlockCursor();
        LoadInventory();
    }

    public void LoadInventory()
    {
        _Inventory = new List<GameObject>();
        _Inventory.Add(DevTestItem);
        _Inventory.Add(DevTestItem2);

        //Get the game manager, get the list of game objects. 
        //_Inventory = GameManager.instance.saveState.GetInventory()
        LoadSpaces();

        int itemCount = 0;
        foreach (GameObject i in _Inventory)
        {
           
            GameObject temp_go = Instantiate(i, _Spaces[itemCount].transform);
           // temp_go.transform.SetParent(_Spaces[itemCount].transform);
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
            Debug.Log("A child was added #" + i);
        }
        return;
    }


    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked){
            Cursor.lockState = CursorLockMode.None;
        }
        if (Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
