using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //get the mesh agent component on the npc
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position); //move towards the target
        }
    }
}
