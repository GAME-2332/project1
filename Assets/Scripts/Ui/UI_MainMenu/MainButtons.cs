using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI_Components {

    public class MainButtons : MonoBehaviour
    {

        public delegate void NewGameClickedAction();
        public static event NewGameClickedAction OnNewGameClicked;

        public delegate void LoadGameClickedAction();
        public static event LoadGameClickedAction OnLoadGameClicked;

        public delegate void OptionsAction();
        public static event OptionsAction OnOptionsClicked;



        [SerializeField]
        Button _newgame;

        [SerializeField]
        Button _options;

        [SerializeField]
        Button _loadgame;

        [SerializeField]
        Button _quit;


        // Start is called before the first frame update
        void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            if (_newgame == null)
            {
                _newgame = transform.GetChild(0).GetComponent<Button>();
            }
            _newgame.onClick.AddListener(OnClickNewGame);
            if(_loadgame == null)
            {
                _loadgame = transform.GetChild(1).GetComponent<Button>();
            }
            _loadgame.onClick.AddListener(OnClickLoadGame);
            if(_options == null)
            {
                _options = transform.GetChild(2).GetComponent<Button>();    
            }
            _options.onClick.AddListener(OnClickOptions);
            if(_quit == null)
            {
                _quit = transform.GetChild(3).GetComponent<Button>();
            }
            _quit.onClick.AddListener(OnClickQuit);
        }

        void OnClickNewGame()
        {
            OnNewGameClicked();
            Debug.Log("newgame");
        }
        void OnClickLoadGame()
        {
            OnLoadGameClicked();
            Debug.Log("load");
        }
        void OnClickOptions()
        {
            Debug.Log("options");
        }
        void OnClickQuit()
        {
            Application.Quit();
        }
    }
}

