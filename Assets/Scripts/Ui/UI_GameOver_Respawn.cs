using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver_Respawn : MonoBehaviour
{
    public Canvas canvas;

    public bool restart = false;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawn;

    public void StateOfGame_Caught()
    {
        canvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        restart = true;
    }

    public void StateOfGame_Respawn()
    {
        restart = true;
        if (restart == true)
        {
            canvas.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            respawnTo();
            
            Physics.SyncTransforms();
        }
    }

    public void respawnTo()
    {
        // instead of "respawn" DO: last saved position
        player.transform.position = respawn.transform.position;
        Debug.Log("Position" + player.transform.position);
    }
}


