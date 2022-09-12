using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

/// <summary>
/// Component that defines which parts of a GameObject to serialize.
/// The component assigns a persistent UID to the object in the editor that is used at runtime.
/// UID code based on an answer by dishmop: https://answers.unity.com/questions/1249093/need-a-persistent-unique-id-for-gameobjects.html
/// </summary>
[ExecuteAlways]
[Serializable]
public class SaveMe : MonoBehaviour, ISerializationCallbackReceiver {
    public static Dictionary<string, SaveMe> uidMap = new Dictionary<string, SaveMe>();

    [ReadOnlyProperty]
    public string identifier;
    [SerializeReference] [ReadOnlyProperty]
    new private Transform transform;
    [SerializeReference] [ReadOnlyProperty]
    private Rigidbody rb;

    public bool savePosition = true;
    public bool saveRotation = true;
    public bool saveScale = false;
    public bool saveRigidbodyForces = false;
    public MonoBehaviour[] saveComponents;

    [SerializeField] [HideInInspector]
    private Vector3 _pos;
    [SerializeField] [HideInInspector]
    private Vector3 _rot;
    [SerializeField] [HideInInspector]
    private Vector3 _scale;
    [SerializeField] [HideInInspector]
    private Vector3 _rbForce;
    [SerializeField] [HideInInspector]
    private string[] _componentData;

    private bool needsRefresh = false;
    private bool firstRefresh = false;

    public void OnBeforeSerialize() {
        // Do nothing in the editor
        if (!Application.IsPlaying(gameObject)) return;

        // Serialize default fields
        if (savePosition) _pos = transform.localPosition;
        if (saveRotation) _rot = transform.localRotation.eulerAngles;
        if (saveScale) _scale = transform.localScale;
        if (saveRigidbodyForces) _rbForce = rb.velocity;

        // Serialize extra component data
        _componentData = new string[saveComponents.Length];
        for (int i = 0; i < saveComponents.Length; i++) {
            if (saveComponents[i] == null) continue;
            _componentData[i] = JsonUtility.ToJson(saveComponents[i]);
        }
    }

    public void OnAfterDeserialize() {
        // Throw out the first deserialization, which only contains reference fields
        needsRefresh = firstRefresh;
        firstRefresh = true;
    }

    void FixedUpdate() {
        // If we're in the editor, ignore the refresh flag: we can't use this check during serialization
        // so we have to do it here instead
        if (Application.IsPlaying(gameObject) && needsRefresh)  {
            transform = GetComponent<Transform>();
            rb = GetComponent<Rigidbody>();

            // Deserialize default fields
            if (savePosition && _pos != null) transform.localPosition = _pos;
            if (saveRotation && _rot != null) transform.localRotation = Quaternion.Euler(_rot);
            if (saveScale && _scale != null) transform.localScale = _scale;
            if (saveRigidbodyForces && _rbForce != null) rb.velocity = _rbForce;

            // Deserialize extra component data
            for (int i = 0; i < saveComponents.Length; i++) {
                if (saveComponents[i] == null || _componentData[i] == null) continue;
                JsonUtility.FromJsonOverwrite(_componentData[i], saveComponents[i]);
            }
        }
        // Reset the refresh flag
        needsRefresh = false;
    }

    // Only assign a UID in the editor build
    #if UNITY_EDITOR
    void Awake() {
        if (transform == null) transform = GetComponent<Transform>();
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // If the object is a prefab (not in a scene), or the game is running, don't assign a UID
        if (Application.IsPlaying(gameObject) || gameObject.scene.name == null) return;
        // If the identifier has not been assigned or it's a duplicate (or otherwise invalid), pick a new one
        if (identifier == null || identifier == "" || (uidMap.ContainsKey(identifier) && uidMap[identifier] != this) || !identifier.StartsWith(gameObject.scene.name)) {
            identifier = gameObject.scene.name + "_" + Guid.NewGuid();
            if (!uidMap.ContainsKey(identifier)) {
                // If adding the new UID is successful
                uidMap.Add(identifier, this);
                EditorUtility.SetDirty(this);
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
        }
    }

    void OnDestroy() {
        // Remove the UID from the map when the object is destroyed
        if (!Application.IsPlaying(gameObject) && identifier != null) uidMap.Remove(identifier, out _);
    }
    #endif
}
