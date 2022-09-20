using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_CopPatrol : MonoBehaviour
{
    public AI_FollowPlayer FollowPlayer;

    public UnityEngine.AI.NavMeshAgent MallCop;

    public Transform[] PatrolTarget;
    public int CurrentPosition = 0;

    private NavMeshAgent AIAgent;

    // Start is called before the first frame update
    void Start()
    {
        AIAgent = GetComponent<NavMeshAgent>();

        CurrentPosition = 0;

        AIAgent.autoBraking = false;

        GotToNextPoint();
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

        //if(transform.position != PatrolTarget[CurrentPosition].position)
        /*if (
               transform.position != PatrolTarget[CurrentPosition].position 
               //&& Vector3.Distance(transform.position, (PatrolTarget[CurrentPosition].position)) < .5
           )
        {*/
            //AIAgent.destination = PatrolTarget[CurrentPosition].position;
            AIAgent.destination = PatrolTarget[CurrentPosition].position;
        //}
        //else 
        //{
            CurrentPosition = (CurrentPosition + 1) % PatrolTarget.Length;
        //}
    }

    public void Patrol()
    {
        if (!AIAgent.pathPending && AIAgent.remainingDistance < 1.0f)
        { 
            GotToNextPoint();
        }

        Debug.Log("Patrol works");
    }
}