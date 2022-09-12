using UnityEngine;
using UnityEditor;

/// <summary>
/// A custom Inspector display for the SaveMe component adding a button to reassign the UID for testing purposes.
/// </summary>
[CustomEditor(typeof(SaveMe))]
public class SaveMeEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        // Button to reassign identifier
        SaveMe saveMe = (SaveMe) target;
        if (GUILayout.Button("Reassign UID")) {
            saveMe.identifier = null;
        }
    }
}