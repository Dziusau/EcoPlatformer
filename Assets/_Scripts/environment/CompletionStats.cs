using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionStats : MonoBehaviour
{
    [SerializeField] private int scoreForOpen = 0;
    [SerializeField] private GameObject completionUI;
    private string levelName;
    private int score;
    private float time;

    private void Start()
    {
        levelName = gameObject.name;
        score = 0;
        time = 0;
    }

    public int ScoreForOpen { get => scoreForOpen; }
    public int Score { get => score; }
    public float Time { get => time; }
    public string LevelName { get => levelName; }

    public void SetValues(int _score, float _time)
    {
        score = _score;
        time = _time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            completionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            completionUI.SetActive(false);
        }
    }
}
