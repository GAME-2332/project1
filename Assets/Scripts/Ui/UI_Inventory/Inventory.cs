using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    List<GameObject> _Inventory;
    // Start is called before the first frame update
    void Start()
    {
        UnlockCursor();
        LoadInventory();
    }

    public void LoadInventory()
    {
       //Get the game manager, get the list of game objects. 
    }

    public void PopulatePage()
    {
        if(_Inventory != null)
        {
            //populate page
        }
        else
        {
            LoadInventory();
        }
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
