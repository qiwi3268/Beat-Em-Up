using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    public GameObject spriteObject;
    private const float enemySpeed = 0.5f;
    private float enemyCurrentSpeed;
    private bool facingRight;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private EnemyState enemyState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();     
        enemyState = GetComponent<EnemyState>();
        animator = spriteObject.GetComponent<Animator>();
        navMeshAgent.speed = enemySpeed;
    }

    private void Update()
    {
        if (enemyState.currentState == EnemyState.currentStateEnum.walk)
            Walk();
        else if (enemyState.currentState == EnemyState.currentStateEnum.idle ) 
            Stop();

        if (enemyState.knockedDown || enemyState.currentState == EnemyState.currentStateEnum.hurt)
            navMeshAgent.speed = 0;
        else
            navMeshAgent.speed = enemySpeed;
    }

    private void Walk()
    {
        if (EnemySight.playerOnRight && facingRight)
        {
            Flip();
        }
        else if (!EnemySight.playerOnRight && !facingRight)
        {
            Flip();
        }

        navMeshAgent.speed = enemySpeed;
        enemyCurrentSpeed = navMeshAgent.velocity.sqrMagnitude;
        navMeshAgent.SetDestination(EnemySight.target.transform.position);
        navMeshAgent.updateRotation = false;
    }

    private void Stop()
    {
        if (EnemySight.playerOnRight && facingRight)
            Flip();
        else if (!EnemySight.playerOnRight && !facingRight)
            Flip();
        navMeshAgent.ResetPath();
    }

    private void Flip()
    {
        animator.SetBool("Attack", false);
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }
}
