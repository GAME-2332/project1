using UnityEngine;

public class GhostHover : MonoBehaviour {
    [Range(0f, 2f)] public float amplitude;
    [Range(0f, 10f)] public float frequency;
    [Range(0f, 1f)] public float exponent;

    private float initialY;

    void Start() {
        initialY = transform.localPosition.y;
    }
    
    private void Update() {
        transform.localPosition = new Vector3(transform.localPosition.x,
            initialY + amplitude * Mathf.Pow(Mathf.Sin(frequency * Time.time), exponent),
            transform.localPosition.z);
    }
}