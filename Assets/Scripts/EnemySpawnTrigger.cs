using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool lastEnemy = false;

    private void Awake()
    {
        enemyPrefab.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemySpawnTrigger")
        {
            enemyPrefab.SetActive(true);
            if (lastEnemy)
            {
                Stats enemyStats = enemyPrefab.GetComponent<Stats>();
                enemyStats.enemyToWin = true;
            }
        }
    }
}
