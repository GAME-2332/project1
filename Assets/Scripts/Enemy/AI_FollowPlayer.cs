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

    

    // Start is called before the first frame update
    void Start()
    {
        MallCop.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {

        Distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);

        if (Distance < minDistance)
        {
            MallCop.SetDestination(Player.position);
            var MallCop_Renderer = this.GetComponent<Renderer>();

            MallCop_Renderer.material.SetColor("_Color", Color.red);
        }

        else if (Distance > minDistance)
        {
            var MallCop_Renderer = this.GetComponent<Renderer>();

            MallCop_Renderer.material.SetColor("_Color", Color.green);

            Patrol.Patrol();
        }
    }
}
