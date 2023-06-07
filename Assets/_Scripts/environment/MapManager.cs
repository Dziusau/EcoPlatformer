using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Cinemachine.DocumentationSortingAttribute;

public class MapManager : MonoBehaviour
{
    private List<Tilemap> levels;
    private List<PlayerData> playerData;
    private int totalScore = 0;

    private void Awake()
    {
        levels = new List<Tilemap>();
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>(true);
        foreach (Tilemap tilemap in tilemaps)
            if (tilemap.CompareTag("Level"))
                levels.Add(tilemap);
    }

    void Start()
    {
        playerData = SaveSystem.LoadPlayersData();
        if (playerData != null)
            foreach (PlayerData completion in playerData)
                totalScore += completion.score;


        foreach (Tilemap level in levels)
        {
            if (level.gameObject.GetComponent<CompletionStats>().ScoreForOpen <= totalScore)
            {
                level.gameObject.SetActive(true);
                if (playerData != null)
                {
                    PlayerData completion = playerData.Find(data => data.levelName == level.name);
                    level.GetComponent<CompletionStats>().SetValues(completion.score, completion.time);
                }
            }
        }

    }
}
