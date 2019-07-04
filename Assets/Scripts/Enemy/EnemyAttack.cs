using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public static float attackRange = 0.23f;
    public static float attackStartDelay = 2.7f;
    public GameObject spriteObject;
    public GameObject attack1Box;
    public GameObject attack2Box;
    public GameObject attack3Box;
    public Sprite attack1SpriteHitFrame;
    public Sprite attack2SpriteHitFrame;
    public Sprite attack3SpriteHitFrame;
    public Sprite currentSprite;
    private NavMeshAgent navMeshAgent;
    private EnemyState enemyState;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyState = GetComponent<EnemyState>();
        animator = spriteObject.GetComponent<Animator>();
    }

    private void Update()
    {
        currentSprite = spriteObject.GetComponent<SpriteRenderer>().sprite;

        if (enemyState.currentState == EnemyState.currentStateEnum.attack)
            Attack();
    }

    private void Attack()
    {
        navMeshAgent.ResetPath();
        attack1Box.gameObject.SetActive(attack1SpriteHitFrame == currentSprite);
        attack2Box.gameObject.SetActive(attack2SpriteHitFrame == currentSprite);
        attack3Box.gameObject.SetActive(attack3SpriteHitFrame == currentSprite);
    }
}
