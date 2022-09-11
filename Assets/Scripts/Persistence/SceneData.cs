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
    public string defaultSpawnPoint;
    public Dictionary<string, GameObject> saveData;

    /// <summary>
    /// Gets the SceneData instance from a given Scene. One is expected to exist in any loaded scene.
    /// </summary>
    public static SceneData Get(Scene scene) {
        return scene.GetRootGameObjects().First(obj => obj.GetComponent<SceneData>() != null).GetComponent<SceneData>();
    }

    void Awake() {
        // In Awake, we define which objects we want to track in the scene; these objects
        // can be either defined manually in the inspector or dynamically if they have the SaveData tag
        if (saveData == null) saveData = new Dictionary<string, GameObject>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SaveData")) {
            saveData.Add(Identifier(obj), obj);
        }
    }

    /// <summary>
    /// Loads any saved player data and sets the player's position to the correct spawn point, if provided.
    /// </summary>
    public void SpawnPlayer(string playerData = null, string spawnPoint = null) {
        if (spawnPoint == null) spawnPoint = defaultSpawnPoint;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnObj = GameObject.FindGameObjectsWithTag("SpawnPoint").First(obj => obj.name == spawnPoint);
        if (playerData != null) JsonUtility.FromJsonOverwrite(playerData, player);
        player.transform.position = spawnObj.transform.position;
    }

    /// <summary>
    /// Writes this scene's data to disk.
    /// </summary>
    /// <param name="root">The save slot's root path, as returned by SaveState#Path</param>
    public void Write(string root) {
        List<string> removed = new List<string>();
        foreach (var (id, obj) in saveData) {
            // If the object has been destroyed, add it to the list of removed objects; otherwise serialize it
            if (obj != null) {
                string path = root + "/" + Path() + "/" + id + ".dat";
                // TODO: More selective save data; possibly a SaveMe component?
                File.WriteAllText(path, JsonUtility.ToJson(obj));
            } else {
                removed.Add(id);
            }
        }
        // Write a list of removed objects to disk to avoid deserializing them
        File.WriteAllLines(root + "/" + gameObject.scene.path + "/removed.dat", removed);
    }

    /// <summary>
    /// Reads this scene's data from disk.
    /// </summary>
    /// <param name="root">The save slot's root path, as returned by SaveState#Path</param>
    public void Read(string root) {
        // If the removed file doesn't exist, the scene hasn't been serialized yet - we can stick to defaults
        if (!File.Exists(root + "/" + gameObject.scene.path + "/removed.dat")) return;
        // Read the list of removed objects from disk
        string[] removed = File.ReadAllLines(root + "/" + gameObject.scene.path + "/removed.dat");
        foreach (var (id, obj) in saveData) {
            // If the object was destroyed before serialization, destroy it again now
            if (!removed.Contains(id)) {
                string path = root + "/" + gameObject.scene.path + "/" + id + ".dat";
                if (File.Exists(path)) {
                    JsonUtility.FromJsonOverwrite(File.ReadAllText(path), obj);
                }
            } else {
                Destroy(obj);
            }
        }
    }

    /// <summary>
    /// Gets the path of the scene this SceneData is attached to.
    /// </summary>
    public string Path() {
        return gameObject.scene.path;
    }

    /// <summary>
    /// Determines an identifier for the given game object.
    /// </summary>
    string Identifier(GameObject gameObject) {
        throw new System.NotImplementedException();
    }

}
