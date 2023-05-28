using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Cinemachine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameObject player; // static refference of player object for the whole scene 

    [Header("Player")]
    public int playerScore;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    [Header("Enemies")]
    [SerializeField] private int maxAmountOfEnemies = 2;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> enemySpawnPositions;
    [SerializeField] private float spawnDelay = 5f;

    [Header("Score")]
    [Range(0, 1)][SerializeField] private float healthValue = 0.5f;
    [Range(0, 1)][SerializeField] private float timeValue = 0.5f;

    private float timer;
    private float nextTimeToSearch;
    private float nextTimeToSpawn;
    private int currentEnemies;
    private Health playerHealth;

    #region Properties

    public int PlayerScore { get => playerScore; }
    public float Timer { get => timer; }
    public Transform PlayerSpawnPoint { get => playerSpawnPoint; set => playerSpawnPoint = value; }

    #endregion


    #region UnityFunctions

    private void Awake()
    {
        FindPlayer();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemies = enemies.Length;
    }

    private void Start()
    {
        playerScore = 0;
        timer = 0f;
        nextTimeToSearch = 0f;
        nextTimeToSpawn = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (player == null)
            FindPlayer();

        if (currentEnemies < maxAmountOfEnemies && nextTimeToSpawn <= Time.time)
            SpawnEnemy();
    }

    #endregion


    #region EnemyFunctions

    public void EnemyDied(GameObject enemy)
    {
        AddScore(1);
        Destroy(enemy);
        currentEnemies--;
        nextTimeToSpawn = Time.time + spawnDelay;
    }

    private void SpawnEnemy()
    {
        // Choose a random spawn point
        Transform spawnPoint = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count - 1)];

        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)];

        // Instantiate a new enemy at the spawn point
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);        

        // Increment the current enemies count
        currentEnemies++;
    }

    #endregion


    #region PlayerFunctions

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }

    public void PlayerDied(GameObject player)
    {
        Destroy(player);
        StartCoroutine(RespawnPlayer());
    }

    private void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                player = searchResult;
                GameObject.FindGameObjectWithTag("CM_vcam").GetComponent<CinemachineVirtualCamera>().Follow = player.transform; // refresh target to be followed by camera  
                playerHealth = player.GetComponent<Health>();
            }
            nextTimeToSearch = Time.time + 0.5f; // try to find player 2 times per second
        }
    }

    public void AddScore(int _score) => playerScore += _score;

    public int CalculateFinalScore()
    {
        playerScore += (int)(playerHealth.CurrentHealth * 10 * healthValue);
        playerScore = (int)(playerScore / (timer * timeValue));

        return playerScore;
    }
    
    #endregion
}
