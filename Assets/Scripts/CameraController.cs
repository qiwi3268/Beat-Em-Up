using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool isFollowing = true;
    public float cameraSpeed;
    private Vector3 playerXPos;
    private GameObject playerTarget;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerXPos = new Vector3 (playerTarget.transform.position.x, 1.958f, -0.866f);

        if (playerXPos.x < -7.0f)
            isFollowing = false;
        else if (playerXPos.x < -6.0f)
            isFollowing = true;

        if (isFollowing)
            transform.position = Vector3.Lerp(transform.position, playerXPos, cameraSpeed);
    }
}