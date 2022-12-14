using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuListener : MonoBehaviour
{
    GameObject EscMenuPrefab;
    GameObject menuPointer;

    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        if(EscMenuPrefab == null)
        {
            EscMenuPrefab = Resources.Load("UI/EscapeMenuPrefab") as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameState == GameManager.GameState.Playing && Input.GetKeyDown(KeyCode.Escape) && isOpen == false) {
            isOpen = true;
            menuPointer = Instantiate(EscMenuPrefab);
        }
        if(menuPointer == null) {
            if (GameManager.instance.gameState == GameManager.GameState.Paused) GameManager.instance.gameState = GameManager.GameState.Playing;
            isOpen = false;
        }
    }
}
