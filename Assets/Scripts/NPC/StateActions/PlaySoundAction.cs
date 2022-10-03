using System;
using UnityEngine;

/// <summary>
/// Plays a sound at a given point or at the player.
/// </summary>
[Serializable]
public class PlaySoundAction : IStateAction {
    [Tooltip("If true, ignores the transform and plays sound directly where the player is.")]
    public bool playAtPlayer;
    public Transform position;
    public AudioClip clip;

    public void Execute() {
        AudioSource.PlayClipAtPoint(clip, playAtPlayer ?
            GameObject.FindWithTag("Player")?.transform.position ?? Vector3.zero : position.position);
    }
}