using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public int maxEnemies = 5; // maximum number of enemies that can be spawned at a time
    public float spawnInterval = 5f; // time between enemy spawns
    public GameObject enemyPrefab; // prefab of the enemy to spawn
    public Transform[] spawnPoints; // array of spawn points for enemies
    //public PlayerHealth playerHealth; // reference to the player's health component
    public int startingHealth = 3; // starting health of the player

    private int currentEnemies; // current number of enemies spawned
    private float spawnTimer; // timer for enemy spawning

    private void Start()
    {
        // Set player starting health
        //playerHealth.SetHealth(startingHealth);
    }

    private void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            // Increment spawn timer
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                // Reset spawn timer and spawn a new enemy
                spawnTimer = 0f;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate a new enemy at the spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Increment the current enemies count
        currentEnemies++;
    }

    public void EnemyKilled()
    {
        // Decrement the current enemies count
        currentEnemies--;
    }

    public void PlayerDied()
    {
        // Handle player death
        Debug.Log("Player died!");
    }
}
