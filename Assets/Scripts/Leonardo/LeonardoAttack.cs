using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonardoAttack : MonoBehaviour
{
    private const float attack1Time = 0.4f;
    private const float attack2Time = 0.35f;
    private const float attack3Time = 0.45f;
    private const float runAttackForce = 80;
    private const float runAttackTime = 0.4f;
    private float attackTimeCounter;
    private int numberOfAttack = 0;
    private Rigidbody rigidBody;
    private Animator animator;
    private LeonardoState leoState;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        leoState = GetComponent<LeonardoState>();
    }

    private void FixedUpdate()
    {
        attackTimeCounter -= Time.deltaTime;      
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackTimeCounter < 0.5 
            && !LeonardoFall.knockedDown && !LeonardoFall.tookDamage)
        {
            LeonardoMovement.walkTimeCounter = 0;
            if (leoState.currentState != leoState.runJumpState
                && leoState.currentState != leoState.jumpState)
                StartCoroutine(leoState.currentState == leoState.runState ? RunAttack() : Attack());
            attackTimeCounter = 1;
        }
    }

    private IEnumerator Attack()
    {
        if (attackTimeCounter < 0)
            numberOfAttack = 0;
        switch (numberOfAttack)
        {
            case 0:
                animator.Play("Attack1");
                yield return new WaitForSeconds(attack1Time);
                numberOfAttack++;
                break;
            case 1:
                animator.Play("Attack2");
                yield return new WaitForSeconds(attack2Time);
                numberOfAttack++;
                break;
            case 2:
                animator.Play("Attack3");
                yield return new WaitForSeconds(attack3Time);
                numberOfAttack = 0;
                break;
        }
        animator.Play("Idle");
    }

    private IEnumerator RunAttack()
    {
        animator.Play("RunAttack");
        numberOfAttack = 0;
        rigidBody.AddForce(transform.right * (LeonardoMovement.facingRight ? 1 : -1) * runAttackForce, ForceMode.Impulse);
        yield return new WaitForSeconds(runAttackTime);
        animator.Play("Idle");
    }
}
