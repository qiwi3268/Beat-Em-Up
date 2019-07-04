using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public bool knockDownAttack;
    public float attackStrength;
    private GameObject otherObject;
    private Stats otherStats;
    private EnemyState enemyState;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "PlayerAttackBox" && other.tag == "EnemyHitBox")
            EnemyTakeDamage(other.gameObject);
        else if (gameObject.tag == "EnemyAttackBox" && other.tag == "PlayerHitBox")
            PlayerTakeDamage(other.gameObject);
        else
            return;
    }

    private void EnemyTakeDamage(GameObject other)
    {
        otherObject = other.transform.parent.gameObject;
        enemyState = otherObject.GetComponent<EnemyState>();
        otherStats = otherObject.GetComponent<Stats>();

        if (!enemyState.knockedDown)
        { 
            otherStats.health -= attackStrength;

            if (knockDownAttack)
                enemyState.knockedDown = true;
            else
                enemyState.tookDamage = true;
        }
    }

    private void PlayerTakeDamage(GameObject other)
    {
        otherObject = other.transform.parent.gameObject;
        otherStats = otherObject.GetComponent<Stats>();
        otherStats.health -= attackStrength;
 
        if (knockDownAttack)
            LeonardoFall.knockedDown = true;
        else
            LeonardoFall.tookDamage = true;
    }
}
