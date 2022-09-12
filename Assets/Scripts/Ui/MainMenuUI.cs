using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    public Button new_game_button;
    [SerializeField]
    public Button load_game_button;
    [SerializeField]
    public Button options_button;
    [SerializeField]
    public Button quit_button;
    [SerializeField]
    public Button credits_button;

    /*
     This script manages the buttons on the title screen

    It allows the player to play a new game
    It loads a window for saved games
    It loads a window for options
    It loads a window to show credits
     
     */

    // Start is called before the first frame update
    void Start()
    {
        //See if buttons have been referenced to the script in the editor
        //if not, run a function to set them up.
        if(new_game_button == null || 
            load_game_button == null || 
            options_button == null || 
            quit_button == null || 
            credits_button == null)
        {
            Debug.Log("Setting up buttons...");
            SetUpButtons();
        }
        new_game_button.onClick.AddListener(OnClickNewGame);
        load_game_button.onClick.AddListener(OnClickLoadGame);
        options_button.onClick.AddListener(OnClickOptions);
        quit_button.onClick.AddListener(OnClickQuit);
        credits_button.onClick.AddListener(OnClickCredits);
    }

    void SetUpButtons()
    {
        new_game_button = GameObject.Find("NewGameButton").GetComponent<Button>();
        load_game_button = GameObject.Find("LoadGameButton").GetComponent<Button>();
        options_button = GameObject.Find("OptionsButton").GetComponent<Button>();
        quit_button = GameObject.Find("QuitButton").GetComponent<Button>();
        credits_button = GameObject.Find("CreditsButton").GetComponent<Button>();
    }

    void OnClickNewGame()
    {
        Debug.Log("You have started a new game.");
        SceneManager.LoadScene("Assets/Scenes/DevScene.unity");
    }
    void OnClickLoadGame()
    {
        Debug.Log("You are Loading a game");
    }

    void OnClickOptions()
    {
        Debug.Log("Options.");
       
    }

    void OnClickQuit()
    {
        Debug.Log("Quitting Game.");
        Application.Quit();
    }

    void OnClickCredits()
    {
        Debug.Log("Credits");
    }

}
