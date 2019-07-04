using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{ 
    public enum currentStateEnum { idle = 0, walk = 1, attack = 2, hurt = 3, knockedDown = 4 };
    public currentStateEnum currentState;
    public GameObject spriteObject;   
    public bool tookDamage;
    public bool knockedDown;
    private const float stunTime = 0.2f;
    private const float knockedDownTime = 2;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AnimatorStateInfo currentStateInfo;
    private Stats enemyState;
    private static int currentAnimState;
    private static int idleState = Animator.StringToHash("Base Layer.Idle");
    private static int walkState = Animator.StringToHash("Base Layer.Walk");
    private static int attack1State = Animator.StringToHash("Base Layer.Attack1");
    private static int attack2State = Animator.StringToHash("Base Layer.Attack2");
    private static int attack3State = Animator.StringToHash("Base Layer.Attack3");
    private static int hurtState = Animator.StringToHash("Base Layer.Hurt");

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
        animator = spriteObject.GetComponent<Animator>();
        enemyState = GetComponent<Stats>();
        enemyState.health = 12;
    }

    private void Update()
    {
        // get knocked down
        if (knockedDown && !tookDamage)
        {
            animator.SetBool("Knocked Down", true);
            StartCoroutine(KnockedDown());
        }
        // take damage
        else if (tookDamage && !knockedDown)
        {
            if (enemyState.health <= 0)
            { 
                animator.SetBool("Knocked Down", true);
                StartCoroutine(KnockedDown());
            }
            else
            { 
                animator.SetBool("Is Hit", true);
                StartCoroutine(TookDamage());
            }
        }
        // attack logic
        else if (!tookDamage && EnemySight.playerInSight &&
            !LeonardoFall.knockedDown &&
            EnemySight.targetDistance < EnemyAttack.attackRange &&
            navMeshAgent.velocity.sqrMagnitude < EnemyAttack.attackStartDelay)
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
        }
        // walk to player
        else if (!knockedDown && !tookDamage && EnemySight.playerInSight
            && !LeonardoFall.knockedDown)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
        }
        //idle
        else if (!tookDamage && !EnemySight.playerInSight
            || LeonardoFall.knockedDown)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
        }

        if (currentAnimState == idleState)
            currentState = currentStateEnum.idle;
        else if (currentAnimState == walkState)
            currentState = currentStateEnum.walk;
        if (currentAnimState == attack1State)
            currentState = currentStateEnum.attack;
        if (currentAnimState == hurtState)
            currentState = currentStateEnum.hurt;

        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currentAnimState = currentStateInfo.fullPathHash;
    }

    private IEnumerator TookDamage()
    {
        animator.Play("Hurt");
        yield return new WaitForSeconds(stunTime);
        animator.SetBool("Is Hit", false);
        tookDamage = false;
    }

    private IEnumerator KnockedDown()
    {
        animator.Play("Fall");
        yield return new WaitForSeconds(knockedDownTime);

        if (enemyState.health <= 0)
            Destroy(gameObject.transform.parent.gameObject);

        animator.SetBool("Knocked Down", false);
        knockedDown = false;      
    }
}
