using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles serialization and deserialization of SceneData and other objects independent of a loaded scene.
/// </summary>
public class SaveState {
    private const int startScene = 1;
    private readonly int slot;
    private SceneData currentScene;
    private string playerData;
    private string spawnPoint;

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
        SceneManager.activeSceneChanged += OnSceneLoaded;
        playerData = File.Exists(Path() + "/player.dat") ? File.ReadAllText(Path() + "/player.dat") : null;
        if (File.Exists(Path() + "/scene.dat")) LoadScene(sceneIndex: int.Parse(File.ReadAllText(Path() + "/scene.dat")));
        else LoadScene(sceneIndex: startScene);
    }

    /// <summary>
    /// Loads the given scene by path or build index, serializing the active one and deserializing the new one.
    /// </summary>
    public void LoadScene(string scenePath = null, int sceneIndex = -1, string spawnPoint = null) {
        // If another game scene is loaded (not the menu), write it to disk first
        playerData = null;
        this.spawnPoint = spawnPoint;
        if (currentScene != null) {
            currentScene.Write(Path());
            var player = GameObject.FindObjectOfType<PlayerMovement>();
            if (player != null) {
                Directory.CreateDirectory(Path());
                playerData = JsonUtility.ToJson(player.playerData);
                File.WriteAllText(Path() + "/player.dat", playerData);
            }
        }

        // If a scene path is given, load it by path; otherwise load it by index
        if (scenePath != null) SceneManager.LoadScene(scenePath);
        else SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// We need to listen for the new scene being loaded separately because SceneManager#LoadScene queues the new scene to load in the following frame.
    /// We can't just use Update because this class isn't a MonoBehaviour.
    /// </summary>
    public void OnSceneLoaded(Scene old, Scene current) {
        // Set up the newly loaded scene
        currentScene = SceneData.Get(SceneManager.GetActiveScene());
        if (currentScene == null) return; // Edge case in case we caught a scene load before SaveState#Load was called
        currentScene.Read(Path());
        currentScene.SpawnPlayer(playerData, spawnPoint);

        // Save the now-current scene index to disk
        Directory.CreateDirectory(Path());
        File.WriteAllText(Path() + "/scene.dat", SceneManager.GetActiveScene().buildIndex.ToString());

        playerData = spawnPoint = null;
    }

    /// <summary>
    /// Called to serialize the current scene without loading another one; ie when the application is closed without switching scenes first.
    /// </summary>
    public void SaveCurrent() {
        if (currentScene != null) currentScene.Write(Path());
        var player = GameObject.FindObjectOfType<PlayerMovement>();
        Directory.CreateDirectory(Path());
        if (player != null) File.WriteAllText(Path() + "/player.dat", JsonUtility.ToJson(player.playerData));
        File.WriteAllText(Path() + "/scene.dat", SceneManager.GetActiveScene().buildIndex.ToString());
    }

    /// <summary>
    /// Gets the path for this save.
    /// </summary>
    public string Path() {
        return Application.persistentDataPath + "/saves/" + slot;
    }
}
