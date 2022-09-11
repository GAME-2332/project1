using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Component for trigger objects to switch to another scene.
/// </summary>
[RequireComponent(typeof(Collider))]
public class SceneSwitch : MonoBehaviour {

    [Tooltip("The scene to switch to.")]
    public SceneReference scene;
    [Tooltip("An ID for a spawn point in the scene being switched to. Must match an ID under a SpawnPoint component in the scene.")]
    public string spawnPoint;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Switch to the specified scene, alerting SceneData of the spawn point to use.
            GameManager.instance.saveState.LoadScene(scenePath: scene.ScenePath, spawnPoint: spawnPoint);
        }
    }

}
