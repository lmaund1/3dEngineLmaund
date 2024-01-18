using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //parameters
    [SerializeField]float chaseRange = 5f;
    float distanceToTarget = Mathf.Infinity;
    [SerializeField] float turnSpeed = 5f;
    
    //cached references
    [SerializeField] Transform playerTarget;
    NavMeshAgent nWA;

    //states
    bool isAggro = false;

    // Start is called before the first frame update
    void Start()
    {
        nWA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(playerTarget.position, transform.position);

        if (isAggro)
        {
            EngageTarget();
        }


        else if(distanceToTarget <= chaseRange)
        {
            isAggro = true;
        }
        
    }

    private void EngageTarget()
    {

        FaceTarget();

        if (distanceToTarget >= nWA.stoppingDistance)
        {
            ChasePlayer();
        }

        else if(distanceToTarget <= nWA.stoppingDistance)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        print("attacking");
        GetComponent<Animator>().SetBool("isAttacking", true);
    }

    private void ChasePlayer()
    {
        GetComponent<Animator>().SetBool("isMoving", true);
        GetComponent<Animator>().SetBool("isAttacking", false);
        nWA.SetDestination(playerTarget.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void FaceTarget()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken()
    {
        isAggro = true;
    }
}
