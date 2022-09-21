using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Interactible{
    public override void Interact() {
        transform.position += new Vector3(0, 1, 0);
    }
}
