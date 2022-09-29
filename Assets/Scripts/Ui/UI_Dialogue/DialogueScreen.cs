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

    public float bottomOffset;
    public float horizontalPadding;
    public float verticalPadding;
    public float horizontalTextPadding;
    public float verticalTextPadding;
    public float horizontalOptionTextPadding;
    public float verticalOptionTextPadding;
    public Vector2 portraitOffset;
    public Vector2 portraitScale;

    private Canvas canvas;
    private Image portrait;

    // private Panel
    private string npcName;
    private DialogueContext ctx;
    private DialogueText npcText;
    private DialogueOption[] instances;

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
        npcText = transform.Find("NPC Text").GetComponent<DialogueText>();
    }

    public void Open(string npcName, DialogueTree tree) {
        GameManager.instance.gameState = GameManager.GameState.Dialogue;
        
        this.npcName = npcName;
        ctx = tree.Traverse(RefreshNPCText, RefreshOptions, SetPortrait, Close);
        canvas.enabled = true;
        
        npcText.SetContext(ctx);
        npcText.SetTextPadding(horizontalTextPadding, verticalTextPadding);
        npcText.SetPosition(0, bottomOffset + verticalPadding);

        ctx.Ready();
    }

    void RefreshNPCText(string text) {
        npcText.SetText(text);
    }

    void RefreshOptions(string[] options) {
        // Some of this is probably redundant, but I don't have the time to clean it up.
        // Should be harmless...
        DestroyOptions();
        if (options == null || options.Length == 0) {
            instances = null;
            npcText.SetPosition(0, bottomOffset);
            return;
        }

        float height = optionPrefab.GetComponent<DialogueOption>().Height;
        float offset = bottomOffset;
        instances = new DialogueOption[options.Length];
        for (int i = 0; i < options.Length; i++) {
            Debug.Log(offset);
            Debug.Log("height " + height);
            CreateOption(i, options[i], offset);
            offset += verticalPadding + height;
        }
        
        npcText.SetPosition(0, offset);
    }

    void CreateOption(int index, string text, float offset) {
        DialogueOption opt = instances[index] = Instantiate(optionPrefab, transform).GetComponent<DialogueOption>();

        opt.Index = index;
        opt.SetContext(ctx);
        opt.SetText(text);
        opt.SetTextPadding(horizontalOptionTextPadding, verticalOptionTextPadding);
        opt.SetPosition(-npcText.Width / 2, offset);
    }

    void SetPortrait(Sprite sprite) {
        portrait.sprite = sprite;
        float width = sprite.rect.width;
        float height = sprite.rect.height;
        
        portrait.rectTransform.anchoredPosition = new Vector2(-npcText.Width / 2 - horizontalPadding, bottomOffset) + portraitOffset;
        portrait.rectTransform.sizeDelta = new Vector2(width, height) * portraitScale;
    }

    void Close() {
        npcText.SetPosition(0, bottomOffset);
        DestroyOptions();
        
        GameManager.instance.gameState = GameManager.GameState.Playing;
        canvas.enabled = false;
    }

    void DestroyOptions() {
        if (instances == null) return;
        foreach (DialogueOption option in instances) {
            Destroy(option.gameObject);
        }
        instances = null;
    }

    private void OnDestroy() {
        if (_instance == this) _instance = null;
    }
}