using UnityEngine;

public abstract class Interactible : MonoBehaviour {
    public static Outline.Mode outlineMode = Outline.Mode.OutlineAndSilhouette;
    public static Color outlineColor = Color.red;
    public static float outlineWidth = 5f;

    private Outline outlineComponent;

    void Start() {
        outlineComponent = gameObject.AddComponent<Outline>();
        // Quick and dirty fix in case an outline component already exists
        if (outlineComponent == null) outlineComponent = GetComponent<Outline>();
        outlineComponent.enabled = false;

        outlineComponent.OutlineMode = outlineMode;
        outlineComponent.OutlineColor = outlineColor;
        outlineComponent.OutlineWidth = outlineWidth;
    }

    public virtual void SetOutline(bool enabled) {
        outlineComponent.enabled = enabled;
    }
    
    public abstract void Interact();
}