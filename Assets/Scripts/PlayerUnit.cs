using UnityEngine;
using System.Collections;

//------------------------------------------------------------------------------
// QUIZ 
// WRITTEN BY: JEREMY POULIN (40112762)
// FOR COMP376 - FALL 2022
//------------------------------------------------------------------------------

public class PlayerUnit : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void SetDestination(Vector3 location)
    {
        navMeshAgent.SetDestination(location);
    }
}
