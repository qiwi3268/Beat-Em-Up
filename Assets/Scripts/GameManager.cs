using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyArray;
    public static GameManager instance = null;
    private const float resetTime = 2.0f;
    private const float timeSlowDown = 0.05f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyArray.Length == 0)
            CameraController.isFollowing = true;
    }

    public void GameOver()
    {
        Invoke("Reset", resetTime);
    }

    private void Reset()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
