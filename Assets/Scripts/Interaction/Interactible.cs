using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour {
    public static Outline.Mode outlineMode = Outline.Mode.OutlineAndSilhouette;
    public static Color outlineColor = Color.red;
    public static float outlineWidth = 5f;

    private Outline outlineComponent;

    void Awake() {
        outlineComponent = gameObject.AddComponent<Outline>();
        outlineComponent.enabled = false;

        outlineComponent.OutlineMode = outlineMode;
        outlineComponent.OutlineColor = outlineColor;
        outlineComponent.OutlineWidth = outlineWidth;
    }

    public void SetOutline(bool enabled) {
        outlineComponent.enabled = enabled;
    }
    
    public abstract void Interact();
}