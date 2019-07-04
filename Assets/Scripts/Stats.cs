using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float health;
    public Slider healthSlider;
    public GameObject healthUI;
    public bool enemyToWin;
    private const float startingHealth = 25;

    private void Update()
    {
        if (gameObject.tag == "Player")
        {
            healthUI = GameObject.FindGameObjectWithTag("PlayerHealthUI");
            healthSlider = healthUI.gameObject.transform.GetChild(0).GetComponent<Slider>();
            if (healthSlider.maxValue == 0)
                healthSlider.maxValue = startingHealth;
            healthSlider.value = health;
        }

        if (health <= 0 && enemyToWin)
        {   
            Time.timeScale = 0.5f;
            GameManager.instance.GameOver();
        }
    }
}
