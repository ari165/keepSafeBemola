using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BaseController : MonoBehaviour
{
    public bool godMode;
    public int Health = 100;
    public HealthBar healthBar;
    public AudioSource hitSound;
    
    public int score;
    public int coins;

    private void Start()
    {
        // randomly change its color
        // TODO: gives ugly colors
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        if (healthBar == null)
            return;
        // set the max value of the health bar
        healthBar.SetMaxHealth(Health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if god mode is enabled, dont check for collisions
        if (godMode)
        {
            return;
        }

        String colTag = collision.collider.tag;
        
        if (colTag == "Enemy")
        {
            // play hit sound
            hitSound.Play();
            
            // decrease health by 10 or 20 and update ui 
            Health -= colTag == "Enemy" ? 10 : 20;
            healthBar.SetHealth(Health);
            
            if (Health <= 0)
            {
                // set the score and high score and save them
                PlayerPrefs.SetFloat("score", score);
                var stats = SaveSystem.LoadStats();

                if (stats != null)
                {
                    coins = stats.coins + score;
                    SaveSystem.SaveStats(coins, stats.highScore < score ? score : stats.highScore);
                }
                else
                {
                    SaveSystem.SaveStats(score, score);
                }
                
                // load the next scene
                SceneManager.LoadScene("Lost");
            }
        }
    }
}
