using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_CopPatrol : MonoBehaviour
{
    public AI_FollowPlayer FollowPlayer;

    public UnityEngine.AI.NavMeshAgent MallCop;

    public Transform[] PatrolTarget;
    public int CurrentPosition;

    public NavMeshAgent AIAgent;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPosition = 0;

        AIAgent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    public void GotToNextPoint()
    {
        if (PatrolTarget.Length == 0)
        {
            return;
        }
        
        if (Vector3.Distance(transform.position, (PatrolTarget[CurrentPosition].position)) < .5)
        {
            //AIAgent.destination = PatrolTarget[CurrentPosition].position;
            MallCop.SetDestination(PatrolTarget[CurrentPosition].position);
        }
        else 
        {
            CurrentPosition = (CurrentPosition + 1) % PatrolTarget.Length;
        }
    }

    public void Patrol()
    {
        //if (AIAgent.remainingDistance < 1.0f)
        //{ 
            GotToNextPoint();
        //}

        Debug.Log("Patrol works");
    }
}
