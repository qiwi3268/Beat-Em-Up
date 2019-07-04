using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public static bool playerInSight;
    public static bool playerOnRight;
    public static float targetDistance;
    public static GameObject target;
    public GameObject player;
    private Vector3 playerRelativePosition;
    private GameObject frontTarget;
    private GameObject backTarget;
    private float frontTargetDistance;
    private float backTargetDistance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        frontTarget = GameObject.Find("Enemy Front Target");
        backTarget = GameObject.Find("Enemy Back Target");
    }

    private void Update()
    {
        playerRelativePosition = player.transform.position - gameObject.transform.position;
        playerOnRight = playerRelativePosition.x > 0;

        frontTargetDistance = Vector3.Distance(frontTarget.transform.position, gameObject.transform.position);
        backTargetDistance = Vector3.Distance(backTarget.transform.position, gameObject.transform.position);

        if (frontTargetDistance < backTargetDistance)
            target = frontTarget;
        else if (frontTargetDistance > backTargetDistance)
            target = backTarget;

        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }
}