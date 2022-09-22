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

        AIAgent.autoBraking = true;

        GotToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        //FollowPlayer.facedirection();
    }

    public void GotToNextPoint()
    {
        if (PatrolTarget.Length == 0)
        {
            return;
        }

        AIAgent.destination = PatrolTarget[CurrentPosition].position;

        CurrentPosition = (CurrentPosition + 1) % PatrolTarget.Length;

    }

    public void Patrol()
    {
        if (!AIAgent.pathPending && AIAgent.remainingDistance < 1.0f)
        {
            GotToNextPoint();
            return;
        }

        //Debug.Log("Patrol works");
    }
}
