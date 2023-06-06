using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionStats : MonoBehaviour
{
    private string levelName;
    private int score;
    private float time;

    public void SetValues(string _levelName, int _score, float _time)
    {
        levelName = _levelName;
        score = _score;
        time = _time;
    }
}
