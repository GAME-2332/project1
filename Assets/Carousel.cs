using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Carousel : MonoBehaviour { 
    // I don't even know anymore
    public Transform pivot;
    public int speed;
    [SerializeReference] [PickImpl(typeof(IStatePredicate))]
    public IStatePredicate condition;

    void Update() {
        if (condition.Check()) {
            transform.RotateAround(pivot.position, transform.up, speed * Time.deltaTime);
        }
    }
}
