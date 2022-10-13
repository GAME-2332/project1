using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchAction : IStateAction {
    [Tooltip("The scene to switch to.")]
    public SceneReference scene;
    [Tooltip("An ID for a spawn point in the scene being switched to. Must match an ID under a SpawnPoint component in the scene.")]
    public string spawnPoint;
        
    public void Execute() {
        // If the current scene setup is done
        if (SceneData.Get(SceneManager.GetActiveScene()).Ready()) {
            // Switch to the specified scene, alerting SceneData of the spawn point to use
            GameManager.instance.saveState.LoadScene(scenePath: scene.ScenePath, spawnPoint: spawnPoint);
        }
    }
        
}