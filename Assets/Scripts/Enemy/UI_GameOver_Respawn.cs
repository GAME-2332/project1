using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOver_Respawn : MonoBehaviour
{
    public Canvas canvas;

    public bool restart = false;

    [Tooltip("The scene to switch to.")]
    public SceneReference scene;
    [Tooltip("An ID for a spawn point in the scene being switched to. Must match an ID under a SpawnPoint component in the scene.")]
    public string spawnPoint;

    //[SerializeField] private Transform player;
    //[SerializeField] private Transform respawn;

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
            Debug.Log("StateOfGame_Respawn");
            /*canvas.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;*/

            respawnTo();
            
            Physics.SyncTransforms();
        }
    }

    public void respawnTo()
    {
        Debug.Log("respawnTo works");
        // instead of "respawn" DO: last saved position
        // If the current scene setup is done
        if (SceneData.Get(gameObject.scene).Ready())
        {
            // Switch to the specified scene, alerting SceneData of the spawn point to use
            GameManager.instance.saveState.LoadScene(scenePath: scene.ScenePath, spawnPoint: spawnPoint); // spawnpoint: Jail_SpawnPoint
        }
    }
}


