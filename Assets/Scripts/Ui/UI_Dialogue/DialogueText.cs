using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class DialogueText: MonoBehaviour {
    public RectTransform rect;
    public TextMeshProUGUI text;
    public Image image;
    
    [NonSerialized]
    protected DialogueContext ctx;
    
    public float Width => rect.sizeDelta.x;
    public float Height => rect.sizeDelta.y;

    void Start() {
        image = GetComponent<Image>();
    }

    public void OnClick() {
        ctx.Continue();
    }

    public void SetContext(DialogueContext ctx) {
        this.ctx = ctx;
    }

    public void SetPosition(float left, float bottom) {
        rect.anchoredPosition = new Vector2(left, bottom);
    }

    public void SetTextPadding(float horizontal, float vertical) {
        text.margin = new Vector4(horizontal, vertical, horizontal, vertical);
    }
    
    public void SetText(string text) {
        this.text.SetText(text);
    }
}