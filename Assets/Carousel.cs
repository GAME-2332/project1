using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour, IRuntimeSerialized { 
    // I don't even know anymore
    public int speed;
    public bool spinning;

    public static void FindAndSpin(bool spinning) {
            var carousel = GameObject.FindObjectOfType<Carousel>();
        if (carousel != null) {
            carousel.Spin(spinning);
        }
    }

    public void Spin(bool spinning) {
        this.spinning = spinning;
    }
    
    void Update() {
        if (spinning) {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }

    public class CarouselSpinAction : IStateAction {
        public void Execute() {
            FindAndSpin(true);
        }
    }
}
