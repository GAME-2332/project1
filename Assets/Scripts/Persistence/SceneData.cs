using System.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles serialization and deserialization within a loaded scene.
/// </summary>
public class SceneData : MonoBehaviour {
    public Transform defaultSpawnPoint;
    public Dictionary<string, SaveMe> saveData;

    private bool ready = false;

    /// <summary>
    /// Gets the SceneData instance from a given Scene. One is expected to exist in any loaded scene.
    /// </summary>
    public static SceneData Get(Scene scene) {
        return scene.GetRootGameObjects().First(obj => obj.GetComponent<SceneData>() != null).GetComponent<SceneData>();
    }

    void Awake() {
        #if UNITY_EDITOR
            if (GameManager.instance.saveState == null) {
                GameManager.instance.saveState = new SaveState(-1);
                GameManager.instance.saveState.Load();
            }
        #endif
        // In Awake, we define which objects we want to track in the scene; these objects
        // can be either defined manually in the inspector or dynamically if they have the SaveData tag
        if (saveData == null) saveData = new Dictionary<string, SaveMe>();
        foreach (SaveMe obj in GameObject.FindObjectsOfType<SaveMe>()) {
            saveData.Add(obj.identifier, obj);
        }
    }

    /// <summary>
    /// Loads any saved player data and sets the player's position to the correct spawn point, if provided.
    /// </summary>
    public void SpawnPlayer(string playerData = null, string spawnPoint = null) {
        Transform spawnTransform = spawnPoint != null ? GameObject.Find(spawnPoint).transform : defaultSpawnPoint;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        Debug.Log("player " + player + "   spawntransform " + spawnTransform);
        if (playerData != null) JsonUtility.FromJsonOverwrite(playerData, playerScript.playerData);
        if (spawnTransform != null) {
            playerScript.shouldFixPosition = true;
            playerScript.fixPosition = spawnTransform.position;
        }

        // Set ready shortly after adjusting the player collisions to avoid early collisions triggering unwanted behavior
        Invoke("SetReady", 0.25f);
    }

    private void SetReady() {
        ready = true;
    }

    /// <summary>
    /// Returns true after the scene has been set up and the player moved to the correct position.
    /// Used to avoid checking early collisions.
    /// </summary>
    public bool Ready() {
        return ready;
    }

    /// <summary>
    /// Writes this scene's data to disk.
    /// </summary>
    /// <param name="root">The save slot's root path, as returned by SaveState#Path</param>
    public void Write(string root) {
        List<string> removed = new List<string>();
        Directory.CreateDirectory(root + "/" + Path());
        foreach (var (id, obj) in saveData) {
            // If the object has been destroyed, add it to the list of removed objects; otherwise serialize it
            if (obj != null && obj.gameObject != null) {
                string path = root + "/" + Path() + "/" + id;
                File.WriteAllText(path + ".dat", JsonUtility.ToJson(obj));
                // Save other serializable components
                foreach (Component component in obj.GetComponents(typeof(Component)).Where(c => c is IRuntimeSerialized)) {
                    File.WriteAllText(path + "." + component.GetType().Name + ".dat", JsonUtility.ToJson(component));
                }
            } else {
                removed.Add(id);
            }
        }
        // Write a list of removed objects to disk to avoid deserializing them
        File.WriteAllLines(root + "/" + Path() + "/removed.dat", removed);
    }

    /// <summary>
    /// Reads this scene's data from disk.
    /// </summary>
    /// <param name="root">The save slot's root path, as returned by SaveState#Path</param>
    public void Read(string root) {
        // If the removed.dat file doesn't exist, the scene hasn't been serialized yet - we can stick to defaults
        if (!File.Exists(root + "/" + Path() + "/removed.dat")) return;
        // Read the list of removed objects from disk
        string[] removed = File.ReadAllLines(root + "/" + Path() + "/removed.dat");
        foreach (var (id, obj) in saveData) {
            // If the object was destroyed before serialization, destroy it again now
            if (!removed.Contains(id)) {
                string path = root + "/" + Path() + "/" + id;
                if (File.Exists(path + ".dat")) {
                    obj.deserializedBySceneData = true;
                    JsonUtility.FromJsonOverwrite(File.ReadAllText(path + ".dat"), obj);
                    foreach (Component component in obj.GetComponents(typeof(Component)).Where(c => c is IRuntimeSerialized)) {
                        if (File.Exists(path + "." + component.GetType().Name + ".dat"))
                            JsonUtility.FromJsonOverwrite(File.ReadAllText(path + "." + component.GetType().Name + ".dat"), component);
                    }
                }
            } else {
                Destroy(obj.gameObject);
            }
        }
    }

    /// <summary>
    /// Gets the path of the scene this SceneData is attached to.
    /// </summary>
    public string Path() {
        // Strip "Assets/Unity/" and ".unity" from the scene path; replace any slashes with periods
        return gameObject.scene.path.Substring(14, gameObject.scene.path.Length - 20).Replace('/', '.');
    }
}
