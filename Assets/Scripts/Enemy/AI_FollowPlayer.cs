using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_FollowPlayer : MonoBehaviour
{
    public UI_GameOver_Respawn ui_respawn;

    public AI_CopPatrol Patrol;
    [SerializeField]
    public EnemyMoosic ChaseMusic;

    public UI_GameOver_Respawn respawn;

    public UnityEngine.AI.NavMeshAgent MallCop;
    public Transform TargetPlayer;

    public float minDistance = 5;

    public float Distance;

    private float ViewAngle;

    public float CheckViewAngle;

    public LayerMask ObstacleMask;

    public bool isHit;

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
        MallCop = GetComponent<NavMeshAgent>();

        ChaseMusic = GetComponent<EnemyMoosic>();

        MallCop.autoBraking = true;

        CheckViewAngle = 75.0f;

        isHit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        FieldOfView();

        Distance = Vector3.Distance(TargetPlayer.transform.position, transform.position); //GameObject.FindWithTag("Player")

        Vector3 PlayerDirection = TargetPlayer.position - transform.position;

        RayCastPlayerDetection();

        if (Distance < minDistance && ViewAngle < CheckViewAngle && isHit == true)
        {
            Patrol.shouldPatrol = false;
            MallCop.SetDestination(TargetPlayer.position);
            ChaseMusic.AudioPlay(); //Plays Chase Music
            Debug.Log("NowplayingAUdioPlay");
            if (Distance <= 1.5f)
            {
                ui_respawn.StateOfGame_Caught();
                // TODO: Player death
            }

            /*var MallCop_Renderer = this.GetComponent<Renderer>();
            MallCop_Renderer.material.SetColor("_Color", Color.red);*/

            Debug.Log("Angle" + ViewAngle);
            Debug.Log("I SEE YOU");
        }

        else
        {
            ChaseMusic.AudioStop();  //Stops Playing Chase Music
            if (!Patrol.shouldPatrol)
            {
                Patrol.GotToNextPoint();
                Patrol.shouldPatrol = true;
            }

            //Debug.Log("I DON'T");
        }
    }

    void FieldOfView()
    {
        Vector3 PlayerDir = TargetPlayer.position - transform.position;

        ViewAngle = Vector3.Angle(transform.forward, PlayerDir);

        Debug.DrawRay(transform.position, transform.forward * minDistance, Color.red);
        Debug.DrawLine(transform.position, TargetPlayer.position, Color.red);

    }

    void RayCastPlayerDetection()
    {
        Vector3 PlayerDirection = TargetPlayer.position - transform.position;

        if (!Physics.Raycast(transform.position, PlayerDirection, Distance, ObstacleMask))
        {
            Debug.DrawRay(transform.position, TargetPlayer.position, Color.green);
            //Debug.Log("Hit it");
            isHit = true;
        }

        else
        {
            isHit = false;
            Debug.DrawRay(transform.position, TargetPlayer.position, Color.white);
            //Debug.Log("No  Hit");
        }
    }

}
