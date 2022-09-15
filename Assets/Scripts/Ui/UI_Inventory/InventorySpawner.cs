using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject InventoryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventoryPrefab = Resources.Load("UI/Inventory_UI_Prefab") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOptions.inventory.GetKeyDown())
        {
            if(Inventory.GetExists() == true)
            {
                Inventory go = FindObjectOfType<Inventory>();
                go.OnClickExit();
            }
            else
            {
                GameManager.instance.gameState = GameManager.GameState.Inventory;
                GameObject go = Instantiate(InventoryPrefab, transform.parent);
                go.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            }
            
        }
    }
}
