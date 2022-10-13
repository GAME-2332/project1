using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    [SerializeField]
    Button _resumeButton;
    [SerializeField]
    Button _saveButton;
    [SerializeField]
    Button _optionsButton;
    [SerializeField]
    Button _quitButton;

    [SerializeField]
    Button _xOutButton;

    [SerializeField]
    Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.gameState = GameManager.GameState.Paused;
        GetReferences();
        
    }

    void GetReferences()
    {
        Button[] _all = GetComponentsInChildren<Button>();
        Debug.Log("Buttons found:" + _all.Length);
        if(_resumeButton == null)
        {
            _resumeButton = _all[0];
        }
        _resumeButton.onClick.AddListener(OnResume);


        if (_optionsButton == null)
        {
            _optionsButton = _all[1];
        }
        _optionsButton.onClick.AddListener(OnOptions);

        if (_saveButton == null)
        {
            _saveButton = _all[2];
        }
        _saveButton.onClick.AddListener(OnSave);


        if (_quitButton == null)
        {
            _quitButton = _all[3];
        }
        _quitButton.onClick.AddListener(OnQuit);

        if (_xOutButton == null)
        {
            _xOutButton = _all[4];
        }
        _xOutButton.onClick.AddListener(OnX);
    }

    void OnResume() {
        GameManager.instance.gameState = GameManager.GameState.Playing;
        CloseCanvas();
    }

    void OnSave()
    {
        GameManager.instance.saveState.SaveCurrent();
        //"Save game and return to title screen"
        
    }

    void OnOptions()
    {
        //Instantiates Options
        GameObject OptionsDialogue = Resources.Load("UI/OptionsMenuPrefab") as GameObject;
        Instantiate(OptionsDialogue);
        Canvas _canvas;
        _canvas = GetComponent<Canvas>();
        _canvas.sortingOrder = 2;
    }

    void OnQuit()
    {
        GameManager.instance.saveState.SaveCurrent();
        Application.Quit();
        
        //save
        //quit.
    }

    void OnX()
    {
        OnResume();
    }

    void CloseCanvas()
    {
        GameManager.instance.gameState = GameManager.GameState.Playing;
        Object.Destroy(this.gameObject);

    }
}
