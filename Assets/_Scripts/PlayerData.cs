using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int score;
    public float time;
    public string name;

    public PlayerData(int _score = 0, float _time = 0f) 
    {
        score = _score;
        time = _time;
        name = "";
    }
}
