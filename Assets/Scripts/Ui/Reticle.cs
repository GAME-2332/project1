using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public Sprite empty;
    public Sprite dotReticle;
    public Sprite circleReticle;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        GameManager.instance.events.optionsReloadEvent.AddListener(UpdateSprite);
        GameManager.instance.events.gameStateChangeEvent.AddListener(OnGameStateChanged);
    }

    private void UpdateSprite() {
        image.sprite = GameManager.instance.gameOptions.reticleStyle.Value switch {
            GameOptions.ReticleStyle.Dot => dotReticle,
            GameOptions.ReticleStyle.Circle => circleReticle,
            _ => empty
        };
    }

    private void OnGameStateChanged(GameManager.GameState state) {
        image.enabled = state == GameManager.GameState.Playing;
    }
}
