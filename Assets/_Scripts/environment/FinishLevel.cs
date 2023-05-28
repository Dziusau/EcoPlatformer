using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("CompleteLevel", 1f);
        }
    }

    private void CompleteLevel()
    {
        Time.timeScale = 0f;

        finishMenu.SetActive(true);
        int score = gameManager.CalculateFinalScore();
        finishMenu.GetComponent<FinishMenu>().ShowScore(score);

        SaveSystem.SavePlayer(SceneManager.GetActiveScene().name, score, gameManager.Timer);
    }
}
