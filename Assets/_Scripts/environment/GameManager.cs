using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerScore;

    private float timer;

    public int PlayerScore { get => playerScore; }
    public float Timer { get => timer; }

    //public int maxEnemies = 5; // maximum number of enemies that can be spawned at a time
    //public GameObject[] enemyPrefabs; // prefabs of the enemy to spawn
    //public Transform[] spawnPoints; // array of spawn points for enemies

    //private int currentEnemies; // current number of enemies spawned

    //private void Start()
    //{
    //    var enemies = GameObject.FindGameObjectsWithTag("enemy");
    //    currentEnemies = enemies.Length;
    //}

    private void Update()
    {
        timer += Time.deltaTime;
    }

    //private void SpawnEnemy()
    //{
    //    // Choose a random spawn point
    //    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

    //    int index = Random.Range(0, enemyPrefabs.Length);
    //    // Instantiate a new enemy at the spawn point
    //    GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoint.position, Quaternion.identity);

    //    // Increment the current enemies count
    //    currentEnemies++;
    //}

    //public void EnemyKilled() => currentEnemies--;

    private void Awake()
    {
        playerScore = 0;
        timer = 0f;
    }

    public void AddScore(int _score) => playerScore += _score;

    public void EnemyDied(GameObject enemy)
    {
        AddScore(1);
        Destroy(enemy);
    }

    public void PlayerDied()
    {
        // Handle player death
        Debug.Log("Player died!");
        //StartCoroutine(new WaitForSeconds(2));
        // new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
