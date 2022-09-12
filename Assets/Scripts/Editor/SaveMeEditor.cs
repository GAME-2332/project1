using UnityEngine;
using UnityEditor;

/// <summary>
/// A custom Inspector display for the SaveMe component adding a button to reassign the UID for testing purposes.
/// </summary>
[CustomEditor(typeof(SaveMe))]
public class SaveMeEditor : Editor {
    public override void OnInspectorGUI() {
        // Button to reassign identifier
        SaveMe saveMe = (SaveMe) target;
        if (GUILayout.Button("Reassign UID")) {
            // Remove old UID from the global map if it is mapped to this object
            if (SaveMe.uidMap.ContainsKey(saveMe.identifier) && SaveMe.uidMap[saveMe.identifier] == saveMe) {
                SaveMe.uidMap.Remove(saveMe.identifier);
            }
            // SaveMe will pick a new UID next frame
            saveMe.identifier = null;
        }
        DrawDefaultInspector();
    }
}