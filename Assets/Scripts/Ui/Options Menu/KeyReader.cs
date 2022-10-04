using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyReader : MonoBehaviour
{
    [SerializeField]
    string keyname;
    [SerializeField]
    KeyCode keycode;
    [SerializeField]
    string keytostring;
    // Start is called before the first frame update
    void Start()
    {

        if (keyname == "FORWARD")
        {
            keycode = GameManager.instance.gameOptions.forward.Value;

        }
        if (keyname == "LEFT")
        {
            
            keycode = GameManager.instance.gameOptions.left.Value;
        }
        if (keyname == "RIGHT")
        {
           
            keycode = GameManager.instance.gameOptions.right.Value;
        }
        if (keyname == "BACK")
        {
            
            keycode = GameManager.instance.gameOptions.back.Value;
        }
        if (keyname == "JUMP")
        {
            
            keycode = GameManager.instance.gameOptions.jump.Value;
        }
        if (keyname == "SPRINT")
        {
            
            keycode = GameManager.instance.gameOptions.sprint.Value;
        }
        if (keyname == "CROUCH")
        {
           
            keycode = GameManager.instance.gameOptions.crouch.Value;
        }
        if (keyname == "INTERACT")
        {
            keycode = GameManager.instance.gameOptions.interact.Value;
        }
        keytostring = keycode.ToString();

        string s = keytostring;

        if (s.Contains("Alpha"))
        {
            s = s.Substring(5);
        }
        if (s.Contains("Arrow"))
        {

            int arrow_name_length = s.Length - s.IndexOf("Arrow");
            s = s.Substring(0, s.Length - arrow_name_length);
        }

        keytostring = s;
        Debug.Log("read keycode:" + keyname + " with keycode.. " + keytostring);
    }

    public string GetString()
    {
        return keytostring;
    }
}
