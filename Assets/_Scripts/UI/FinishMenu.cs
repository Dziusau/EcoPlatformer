using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishMenu : MonoBehaviour
{
    public void Continue()
    {
        Debug.Log("Load menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }

    public void ShowScore(int _score)
    {
        GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>().text += _score;
    }
}
