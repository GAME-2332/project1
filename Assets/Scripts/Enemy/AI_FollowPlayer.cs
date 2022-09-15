using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_FollowPlayer : MonoBehaviour
{
    public AI_CopPatrol Patrol;

    public UnityEngine.AI.NavMeshAgent MallCop;
    public Transform Player;

    public Transform ClosestPatrolPoint;

    public float minDistance = 5;

    public float Distance;

    private float ViewAngle;

    // Start is called before the first frame update
    void Start()
    {
        MallCop.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        FieldOfView();

        Distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

        if (Distance < minDistance && ViewAngle < 45.0f)
        {
            MallCop.SetDestination(Player.position);
            var MallCop_Renderer = this.GetComponent<Renderer>();

            MallCop_Renderer.material.SetColor("_Color", Color.red);

            print("Angle" + ViewAngle);
            Debug.Log("I SEE YOU");
        }

        else if (Distance > minDistance)
        {
            var MallCop_Renderer = this.GetComponent<Renderer>();

            MallCop_Renderer.material.SetColor("_Color", Color.green);

            //Patrol.Patrol();

            print("Angle" + ViewAngle);
            //Debug.Log("I DON'T");
        }
    }

    void FieldOfView()
    {
        Vector3 PlayerDir = Player.position - transform.position;

        ViewAngle = Vector3.Angle(transform.forward, PlayerDir); // 0-180 degree

        Debug.DrawRay(transform.position, transform.forward * 25, Color.red);
        Debug.DrawLine(transform.position, Player.position, Color.red);

        /*if (ViewAngle < 90.0f)
        {
            Debug.Log("I SEE YOU");
        }
        else
        {
            Debug.Log("I Dont");
        }*/
    }
}
