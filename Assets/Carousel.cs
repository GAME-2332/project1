using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Carousel : MonoBehaviour { 
    // I don't even know anymore
    public Transform pivot;
    public int speed;

    void Update() {
        if (GameManager.instance.saveState.globals.GetInt("power_boxes_fixed") >= 3) {
            transform.RotateAround(pivot.position, transform.up, speed * Time.deltaTime);
        }
    }
}
