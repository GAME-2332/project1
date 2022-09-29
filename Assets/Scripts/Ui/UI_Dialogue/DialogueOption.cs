using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DialogueOption: DialogueText {
    [NonSerialized] public int Index;
    
    public Color normalColor;
    public Color hoverColor;

    public void OnHoverStart() {
        image.color = hoverColor;
    }

    public void OnHoverEnd() {
        image.color = normalColor;
    }

    public new void OnClick() {
        Debug.Log(ctx);
        ctx.Choose(Index);
    }
}