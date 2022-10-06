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

        //raise this event for the keybind manager to check if that key already exists! 
        public delegate void AttemptSetKey(string _newKeybind, string _mykeyID, KeyCode _newkeycode);
        public static event AttemptSetKey OnAttemptSetKey;

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

        [SerializeField]
        KeyReader _keyreader;
        private void OnEnable()
        {
            KeyBinder_Graphics.STARTACTIVATELISTEN += ActivateListening;
            Key_Bind_Manager.OnKeyCheckFinished += FinalSetKey;
        }
        private void OnDisable()
        {
            KeyBinder_Graphics.STARTACTIVATELISTEN -= ActivateListening; 
            Key_Bind_Manager.OnKeyCheckFinished -= FinalSetKey;
        }


        [SerializeField]
        string main_key_id; //it should be same as the keybinder_graphic it is bound to.

        [SerializeField]
        KeyBinder_Graphics _mygraphics;
        // Start is called before the first frame update
        void Start()
        {
            _mygraphics = GetComponent<KeyBinder_Graphics>();
            
            GetReferences();
            SELECTEDINPUT_COLOR = new Color(100f / 256f, 106f / 256f, 255f / 256f, 1f);
        }

        public string GetKeyBind()
        {
            if(_currentText == "")
            {
                _currentText = transform.GetChild(1).GetComponentInChildren<TMPro.TMP_Text>().text;
            }
            return _currentText; 
        }
        void GetReferences()
        {
            if (_inputRegion == null)
            {
                _inputRegion = transform.GetChild(1);
            }
            if(_keyreader == null)
            {
                _keyreader = GetComponent<KeyReader>();
            }
            main_key_id = transform.GetChild(0).GetComponentInChildren<TMPro.TMP_Text>().text;
    

            _text = _inputRegion.GetComponentInChildren<TMPro.TMP_Text>();

            _currentText = _keyreader.GetString();

            _text.text = _currentText;

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
                        I_AM_ACTIVE = false;
                    }

                    else if (Input.GetKey(KeyCode.Escape)) //if you escape, you deselect
                    {
                        
                        _text.text = _currentText;
                        I_AM_ACTIVE = false;
                    }
                    else //on pressing any other key.
                    {
                           
                            SetKey();
                        //remain I_AM_ACTIVE until checks are finished
                    }

                    OnSetListeningState("start graphics interactions");
                    
                    _inputRegion.GetComponentInChildren<Image>().color = DEFAULT_COLOR;
                    
                }
            }
            else
            {
                _text.text = _keyreader.GetString();
            }
        }

        void ActivateListening(string key_id) 
            //first of all, the problem is that ALL of them enter activate listen mode. We only need one of them to!
        {
            if(key_id == main_key_id)
            {
               
                SetAsSelected();
            }

            //for all the keybinds
            OnSetListeningState("stop graphics because we are waiting for input");
        }


        void SetAsSelected()
        {
            I_AM_ACTIVE = true;
           
            _inputRegion.GetComponentInChildren<Image>().color = SELECTEDINPUT_COLOR;
           
            _text.text = "";
        }

        //this is the new keycode you are attempting to set.
        string s;
        void SetKey()
        {
            s = "";
            KeyCode newKeyCode = KeyCode.None; //this will always have something because this function is only called when input is heard
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    newKeyCode = kcode;
                    s = kcode.ToString();

                    break;
                }
            }



            if (s.Contains("Alpha"))
            {
                s = s.Substring(5);
            }
            if (s.Contains("Arrow"))
            {

                int arrow_name_length = s.Length - s.IndexOf("Arrow");
                s = s.Substring(0, s.Length - arrow_name_length);
            }


            OnAttemptSetKey(s, main_key_id, newKeyCode);


        }

        public void FinalSetKey(bool status)
        {
            if(I_AM_ACTIVE == true)
            {
                I_AM_ACTIVE = false;
                if(status == true) //this is a valid new key
                {
                    _text.text = s;
                }
                else
                {
                    _text.text = _currentText;
                }
                _currentText = _text.text; //update current text
            }
        }
    }

}
