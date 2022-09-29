using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DialogueScreen: MonoBehaviour {
    private static GameObject screenPrefab;
    private static GameObject optionPrefab;
    private static DialogueScreen _instance;
    
    public static bool Exists() => _instance != null;

    public static DialogueScreen GetOrCreate() {
        if (!Exists()) {
            _instance = Instantiate(screenPrefab, Vector3.zero, Quaternion.identity).GetComponent<DialogueScreen>();
        }
        return _instance;
    }

    public int horizontalPadding;
    public int verticalPadding;

    private Canvas canvas;
    private Image portrait;

    // private Panel
    private string npcName;
    private DialogueContext ctx;

    void Awake() {
        // We can't call Load in a static initializer, so we do it here instead.
        if (screenPrefab == null) screenPrefab = Resources.Load("UI/Dialogue/DialogueScreen") as GameObject;
        if (optionPrefab == null) optionPrefab = Resources.Load("UI/Dialogue/DialogueOption") as GameObject;
        
        if (Exists() && _instance != this) Destroy(gameObject);
        else _instance = this;
    }

    void Start() {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.enabled = false;
        
        portrait = transform.Find("Portrait").GetComponent<Image>();
    }

    public void Open(string npcName, DialogueTree tree) {
        this.npcName = npcName;
        canvas.enabled = true;
        ctx = tree.Traverse(RefreshOptions, SetPortrait, Close);
    }

    void RefreshOptions(string[] options) {
        // TODO: Method stub
    }

    void SetPortrait(Sprite sprite) {
        portrait.sprite = sprite;
        
        int width = sprite.texture.width, height = sprite.texture.height;
        portrait.rectTransform.sizeDelta = new Vector2(width, height);
        portrait.rectTransform.anchoredPosition = new Vector2(-width / 2 - horizontalPadding, height / 2 + verticalPadding);
    }

    void Close() {
        canvas.enabled = false;
    }

    private void OnDestroy() {
        if (_instance == this) _instance = null;
    }
}