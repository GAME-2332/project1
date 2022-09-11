using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles serialization and deserialization of SceneData and other objects independent of a loaded scene.
/// </summary>
public class SaveState {
    private const int startScene = 0;
    private int slot;
    private SceneData currentScene;

    /// <summary>
    /// Constructs a new SaveState instance for the given slot.
    /// </summary>
    /// <param name="slot"></param>
    public SaveState(int slot) {
        this.slot = slot;
    }

    /// <summary>
    /// Fetch the save state if it exists and load the last scene, otherwise start from scratch.
    /// </summary>
    public void Load() {
        if (File.Exists(Path() + "/scene.dat")) LoadScene(sceneIndex: int.Parse(File.ReadAllText(Path() + "/scene.dat")));
        else LoadScene(sceneIndex: startScene);
    }

    /// <summary>
    /// Loads the given scene by path or build index, serializing the active one and deserializing the new one.
    /// </summary>
    public void LoadScene(string scenePath = null, int sceneIndex = -1, string spawnPoint = null) {
        // If another game scene is loaded (not the menu), write it to disk first
        string playerData = null;
        if (currentScene != null) {
            currentScene.Write(Path());
            // Idiotproof player data serialization
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerData = JsonUtility.ToJson(player);
        }
        // If a scene path is given, load it by path; otherwise load it by index
        if (scenePath != null) SceneManager.LoadScene(scenePath);
        else SceneManager.LoadScene(sceneIndex);
        File.WriteAllText(Path() + "/scene.dat", SceneManager.GetActiveScene().buildIndex.ToString());
        // Set up the newly loaded scene
        currentScene = SceneData.Get(SceneManager.GetActiveScene());
        currentScene.Read(Path());
        currentScene.SpawnPlayer(playerData, spawnPoint);
    }

    /// <summary>
    /// Called to serialize the current scene without loading another one; ie when the application is closed without switching scenes first.
    /// </summary>
    public void SaveCurrent() {
        if (currentScene != null) currentScene.Write(Path());
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) File.WriteAllText(Path() + "/player.dat", JsonUtility.ToJson(player));
        File.WriteAllText(Path() + "/scene.dat", SceneManager.GetActiveScene().buildIndex.ToString());
    }

    /// <summary>
    /// Gets the path for this save.
    /// </summary>
    public string Path() {
        return Application.persistentDataPath + "saves/" + slot;
    }
}
