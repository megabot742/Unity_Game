using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 8f;
    [SerializeField] float turnSpeed = 10f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if(health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false; // turn off for not error SetDestination
            return;
        }
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
    public void OnDamageTaken()
    {
        isProvoked = true;
    }
    void EngageTarget()
    {
        FaceTarget();
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
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }
    void FaceTarget()
    {
        //Direction
        Vector3 direction = (target.position - transform.position).normalized;
        //Rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        //Debug.Log("You dead!!!");
    }
    void OnDrawGizmosSelected() //Draw enemy chaseRange radius
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);    
    }
}
