using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolling : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    public bool canDetectPlayer;
    public GameObject Player;
    private float playerDistance;
    private bool playerDetected;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerDetected = false;
    }

    void Update()
    {
        
        if (!canDetectPlayer)
        {
            //old code----------------------------------------------------------------------
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                currentPoint++;

                // Reset to first point if we reach the end
                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }

                agent.SetDestination(patrolPoints[currentPoint].position);


            }
            //------------------------------------------------------------------------------
        }
        else
        {
            playerDistance = Vector3.Distance(Player.transform.position, transform.position);
            if (playerDistance <= 3f)
            {
                playerDetected = true;
            }

            if(playerDetected)
            { 
                agent.SetDestination(Player.transform.position);
            }
            else
            {
                //old code----------------------------------------------------------------------
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    currentPoint++;

                    // Reset to first point if we reach the end
                    if (currentPoint >= patrolPoints.Length)
                    {
                        currentPoint = 0;
                    }

                    agent.SetDestination(patrolPoints[currentPoint].position);


                }
                //------------------------------------------------------------------------------
            }
        }

    }
}
