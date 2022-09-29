using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuUI_Components {







    public class KeyBinder : MonoBehaviour
    {

        public delegate void SetListening(string s);
        public static event SetListening OnSetListeningState;

        /*Thre is only one keybinder that is active / I_AM_ACTIVE == true at all times. 
         * Or none. 
         * But never more than 1. 
         * They are identified by their main key id / graphic's key id.
        */
        bool I_AM_ACTIVE;


        [SerializeField]
        Transform _inputRegion;

        [SerializeField]
        Color DEFAULT_COLOR;
        [SerializeField]
        Color SELECTEDINPUT_COLOR;

        [SerializeField]
        TMPro.TMP_Text _text;

        [SerializeField]
        string _currentText;
        private void OnEnable()
        {
            KeyBinder_Graphics.STARTACTIVATELISTEN += ActivateListening;
        }
        private void OnDisable()
        {
            KeyBinder_Graphics.STARTACTIVATELISTEN -= ActivateListening;
        }
        [SerializeField]
        string main_key_id; //it should be same as the keybinder_graphic it is bound to.

        [SerializeField]
        KeyBinder_Graphics _mygraphics;
        // Start is called before the first frame update
        void Start()
        {
            _mygraphics = GetComponent<KeyBinder_Graphics>();
            main_key_id = transform.GetChild(0).GetComponentInChildren<TMPro.TMP_Text>().text;
            GetReferences();
            SELECTEDINPUT_COLOR = new Color(100f / 256f, 106f / 256f, 255f / 256f, 1f);
        }

        void GetReferences()
        {
            if (_inputRegion == null)
            {
                _inputRegion = transform.GetChild(1);
            }
            DEFAULT_COLOR = _inputRegion.GetComponentInChildren<Image>().color;
        }
        // Update is called once per frame
        void Update()
        {
            if(I_AM_ACTIVE == true)
            {
                _inputRegion.GetComponentInChildren<Image>().color = Color.Lerp(SELECTEDINPUT_COLOR, DEFAULT_COLOR, Mathf.PingPong(Time.time, 1));

                if (Input.anyKey == true)
                {

                    if (Input.GetMouseButtonDown(0)) //if you click, you deselect.
                    {
                       
                        _text.text = _currentText;
                    }

                    else if (Input.GetKey(KeyCode.Escape)) //if you escape, you deselect
                    {
                        
                        _text.text = _currentText;
                    }
                    else //on pressing any other key.
                    {
                        
                            Debug.Log("You have set the key to...");
                            SetKey();
                      
                    }

                    OnSetListeningState("start graphics interactions");
                    I_AM_ACTIVE = false;
                    _inputRegion.GetComponentInChildren<Image>().color = DEFAULT_COLOR;
                    
                }
            }
        }

        void ActivateListening(string key_id) 
            //first of all, the problem is that ALL of them enter activate listen mode. We only need one of them to!
        {
            if(key_id == main_key_id)
            {
                Debug.Log("I am active!");
                SetAsSelected();
            }

            //for all the keybinds
            OnSetListeningState("stop graphics because we are waiting for input");
        }


        void SetAsSelected()
        {
            I_AM_ACTIVE = true;
            Debug.Log("Selected!" + main_key_id);
           

            _inputRegion.GetComponentInChildren<Image>().color = SELECTEDINPUT_COLOR;
            _text = _inputRegion.GetComponentInChildren<TMPro.TMP_Text>();
            _currentText = _text.text;
            _text.text = "";
        }


        void SetKey()
        {


            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {

                    Debug.Log("KeyCode down: " + kcode);
                    _text.text = kcode.ToString();

                    break;
                }
            }
            string s = _text.text;
            if (s.Contains("Alpha"))
            {
                s = s.Substring(5);
            }
            if (s.Contains("Arrow"))
            {

                int arrow_name_length = s.Length - s.IndexOf("Arrow");
                s = s.Substring(0, s.Length - arrow_name_length);
            }
            _text.text = s;
        }
    }

}
