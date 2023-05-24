using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
        finishMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
