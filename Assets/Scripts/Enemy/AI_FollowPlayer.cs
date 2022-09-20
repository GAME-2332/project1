using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_FollowPlayer : MonoBehaviour
{
    public AI_CopPatrol Patrol;

    public UnityEngine.AI.NavMeshAgent MallCop;
    public Transform TargetPlayer;

    public Transform ClosestPatrolPoint;

    public float minDistance = 5;

    public float Distance;

    private float ViewAngle;

    public float CheckViewAngle;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minDistance);

        Gizmos.color = Color.blue;
        Vector3 FOV1 = Quaternion.AngleAxis(CheckViewAngle, transform.up) * transform.forward * minDistance;
        Vector3 FOV2 = Quaternion.AngleAxis(-CheckViewAngle, transform.up) * transform.forward * minDistance;

        Gizmos.DrawRay(transform.position, FOV1);
        Gizmos.DrawRay(transform.position, FOV2);
    }

    // Start is called before the first frame update
    void Start()
    {
        MallCop.autoBraking = true;

        CheckViewAngle = 75.0f;
    }

    // Update is called once per frame
    void Update()
    {
        FieldOfView();

        Distance = Vector3.Distance(TargetPlayer.transform.position, transform.position); //GameObject.FindWithTag("Player")

        if (Distance < minDistance && ViewAngle < CheckViewAngle)
        {
            MallCop.SetDestination(TargetPlayer.position);


            if (Distance <= 1.5f)
            { 
                MallCop.velocity = Vector3.zero;
            }
            //yield WaitForSeconds(5);

            /*var MallCop_Renderer = this.GetComponent<Renderer>();
            MallCop_Renderer.material.SetColor("_Color", Color.red);*/

            Debug.Log("Angle" + ViewAngle);
            Debug.Log("I SEE YOU");
        }

        else if (Distance > minDistance)
        {
            var MallCop_Renderer = this.GetComponent<Renderer>();

            MallCop_Renderer.material.SetColor("_Color", Color.green);

            Patrol.Patrol();

            //Debug.Log("Angle" + ViewAngle);
            //Debug.Log("I DON'T");
        }
    }

    void FieldOfView()
    {
        Vector3 PlayerDir = TargetPlayer.position - transform.position;

        ViewAngle = Vector3.Angle(transform.forward, PlayerDir); // 0-180 degree

        Debug.DrawRay(transform.position, transform.forward * minDistance, Color.red);
        Debug.DrawLine(transform.position, TargetPlayer.position, Color.red);


        

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
