using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles serialization and deserialization of SceneData and other objects independent of a loaded scene.
/// </summary>
public class SaveState {
    private const int startScene = 1;

    public readonly int slot;
    public string lastSceneName;
    public string lastSaveDate;
    public string lastSaveTime;

    // Globals and ItemInfo are populated after Load is called
    public Globals globals;
    public PlayerInventory inventory = new PlayerInventory();

    private SceneData currentScene;
    private string playerData;
    private string spawnPoint;

    /// <summary>
    /// Constructs a new SaveState instance for the given slot.
    /// </summary>
    /// <param name="slot"></param>
    public SaveState(int slot) {
        this.slot = slot;
        ReadSceneInfo();
    }

    /// <summary>
    /// Clear all this save state's data and erases its directory.
    /// </summary>
    public void Clear() {
        if (Directory.Exists(Path())) Directory.Delete(Path(), true);
        currentScene = null;
        playerData = null;
        spawnPoint = null;
    }

    /// <summary>
    /// Fetch the save state if it exists and load the last scene, otherwise start from scratch.
    /// </summary>
    public void Load() {
        // Add event listeners
        SceneManager.activeSceneChanged += OnSceneLoaded;
        Application.wantsToQuit += () => {
            SaveCurrent();
            return true;
        };

        // Load basic info, globals and player data
        playerData = File.Exists(Path() + "/player.dat") ? File.ReadAllText(Path() + "/player.dat") : null;
        if (File.Exists(Path() + "/scene.dat")) LoadScene(sceneIndex: int.Parse(File.ReadAllText(Path() + "/scene.dat")));
        else LoadScene(sceneIndex: startScene);

        globals = new Globals();
        if (File.Exists(Path() + "/globals.dat")) JsonUtility.FromJsonOverwrite(File.ReadAllText(Path() + "/globals.dat"), globals);
        if (File.Exists(Path() + "/inventory.dat")) JsonUtility.FromJsonOverwrite(File.ReadAllText(Path() + "/inventory.dat"), inventory);
        GameManager.instance.gameState = GameManager.GameState.Playing;
    }

    /// <summary>
    /// Loads the given scene by path or build index, serializing the active one and deserializing the new one.
    /// </summary>
    internal void LoadScene(string scenePath = null, int sceneIndex = -1, string spawnPoint = null) {
        // If another game scene is loaded (not the menu), write it to disk first
        playerData = null;
        Directory.CreateDirectory(Path());
        File.WriteAllText(Path() + "/globals.dat", JsonUtility.ToJson(globals));
        this.spawnPoint = spawnPoint;
        if (currentScene != null) {
            currentScene.Write(Path());
            var player = GameObject.FindObjectOfType<PlayerMovement>();
            if (player != null) {
                playerData = JsonUtility.ToJson(player.playerData);
                File.WriteAllText(Path() + "/player.dat", playerData);
                WriteSceneInfo();
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
    internal void OnSceneLoaded(Scene old, Scene current) {
        // Set up the newly loaded scene
        currentScene = SceneData.Get(SceneManager.GetActiveScene());
        if (currentScene == null) return; // Edge case in case we caught a scene load before SaveState#Load was called
        currentScene.Read(Path());
        currentScene.SpawnPlayer(playerData, spawnPoint);
        playerData = spawnPoint = null;
    }

    /// <summary>
    /// Called to serialize the current scene without loading another one; ie when the application is closed without switching scenes first.
    /// </summary>
    internal void SaveCurrent() {
        Directory.CreateDirectory(Path());
        File.WriteAllText(Path() + "/globals.dat", JsonUtility.ToJson(globals));
        if (currentScene != null) currentScene.Write(Path());
        var player = GameObject.FindObjectOfType<PlayerMovement>();
        if (player != null) {
            File.WriteAllText(Path() + "/player.dat", JsonUtility.ToJson(player.playerData));
            WriteSceneInfo();
        }
    }

    private void WriteSceneInfo() {
        Directory.CreateDirectory(Path());
        File.WriteAllText(Path() + "/inventory.dat", JsonUtility.ToJson(inventory));
        File.WriteAllText(Path() + "/scene.dat", SceneManager.GetActiveScene().buildIndex.ToString());
        lastSceneName = Util.Prettify(SceneManager.GetActiveScene().name);
        lastSaveDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        lastSaveTime = System.DateTime.Now.ToString("HH:mm:ss");
        File.WriteAllLines(Path() + "/info.dat", new string[] { lastSceneName, lastSaveDate, lastSaveTime });
    }

    private void ReadSceneInfo() {
        if (File.Exists(Path() + "/info.dat")) {
            var lines = File.ReadAllLines(Path() + "/info.dat");
            lastSceneName = lines[0];
            lastSaveDate = lines[1];
            lastSaveTime = lines[2];
        }
    }

    public bool Exists() {
        return lastSaveDate != null && lastSaveTime != null;
    }

    /// <summary>
    /// Gets the path for this save.
    /// </summary>
    public string Path() {
        return Application.persistentDataPath + "/saves/" + slot;
    }
}
