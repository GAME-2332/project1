using MainMenuUI_Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class makes sure there are no repeats in key binds, otherwise, they will revert to original.
public class Key_Bind_Manager : MonoBehaviour
{
    public delegate void FinishedKeyCheck(bool status);
    public static event FinishedKeyCheck OnKeyCheckFinished;
    List<KeyBinder> _keybinds;

    private void OnEnable()
    {
        KeyBinder.OnAttemptSetKey += CheckIfExists;
    }

    private void OnDisable()
    {
        KeyBinder.OnAttemptSetKey -= CheckIfExists;
    }
    // Start is called before the first frame update
    void Start()
    {
        _keybinds = new List<KeyBinder> ();

        KeyBinder[] _k = GetComponentsInChildren<KeyBinder>();
        foreach(KeyBinder k in _k)
        {
            _keybinds.Add (k);
        }
       
    }

    void CheckIfExists(string _newKeyBind, string _main_key_id, KeyCode _newkeycode)
    {
      
        bool validNewKey = true;
        foreach(KeyBinder k in _keybinds)
        {
            if(_newKeyBind == k.GetKeyBind())
            {
                Debug.Log("sorry the key " + _newKeyBind + " is already bound to something else!");
                validNewKey = false;
                //set that key to its old value.
            }
        }
        if (_newkeycode == KeyCode.I)
        {
            Debug.Log("You cannot set keycode to I for that is inventory!");
            validNewKey = false;
        }

        OnKeyCheckFinished(validNewKey);

        //if it is a new and valid key, update that key.
        if(validNewKey == true)
        {
            UpdateKey( _newKeyBind,  _main_key_id, _newkeycode);
        }
        
    }

    void UpdateKey(string _newKeyBind, string _main_key_id, KeyCode _newkeycode )
    {
        Debug.Log("updating key" + _main_key_id + " to new keybind " + _newkeycode.ToString());
        
        GameManager.instance.gameOptions.SetKey(_main_key_id, _newkeycode);
    }

}
