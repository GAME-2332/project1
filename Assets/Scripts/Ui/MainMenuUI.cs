using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        new_game_button.onClick.AddListener(OnClickNewGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickNewGame()
    {

    }
}
