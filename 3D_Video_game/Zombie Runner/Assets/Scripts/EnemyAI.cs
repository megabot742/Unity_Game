using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 8f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked == true)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {   
            isProvoked = true;
        }
    }
    void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }
    void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        //if player too far, enemy don't chase
        // if(distanceToTarget > chaseRange + 10f) 
        // {
        //     isProvoked = false;
        // }
    }
    void AttackTarget()
    {
        Debug.Log("You dead!!!");
    }
    void OnDrawGizmosSelected() //Draw enemy chaseRange radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);    
    }
}
