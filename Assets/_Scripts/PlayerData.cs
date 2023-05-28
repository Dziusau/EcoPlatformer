using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string levelName;
    public int score;
    public float time;

    public PlayerData(string _level = "", int _score = 0, float _time = 0f) 
    {
        levelName = _level;
        score = _score;
        time = _time;
    }
}
