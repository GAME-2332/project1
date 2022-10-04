using TMPro;
using UnityEngine;

public class ActionBar : MonoBehaviour  {
    public TextMeshProUGUI text;

    public static void Show(string text, float duration) {
        FindObjectOfType<ActionBar>().SetText(text);
        FindObjectOfType<ActionBar>().Invoke("ResetText", duration);
    }
    
    void SetText(string text) {
        this.text.text = text;
    }

    void ResetText() {
        FindObjectOfType<ActionBar>().SetText("");
    }
}
