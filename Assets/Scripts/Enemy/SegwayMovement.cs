using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SegwayMovement : MonoBehaviour
{
    public AI_FollowPlayer AIAgent;

    public UnityEngine.AI.NavMeshAgent MallCop;

    private float speed;

    Vector3 movement;

    public Rigidbody rigidbody_AI;

    Transform transform;

    float tiltOnZ;

    public float maxTilt = 20.0f;

    //float angleRange{Random.Range(10.0f, maxTilt)};

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = MallCop.velocity.magnitude;
        float tilt = speed / MallCop.speed;

        tiltOnZ = transform.eulerAngles.z;

        Quaternion target = Quaternion.Euler(0, transform.eulerAngles.y, Mathf.Lerp(0.0f, maxTilt, tilt));

        transform.rotation = Quaternion.Slerp(transform.rotation, target, .5f);
        
        Debug.Log("Z angle: " + tiltOnZ);
        Debug.Log("Speed: " + speed);
    }

}
