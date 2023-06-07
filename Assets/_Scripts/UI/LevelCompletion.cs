using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] private Collider2D levelObject;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        // Access and set the previous completion data
        float previousTime = levelObject.GetComponent<CompletionStats>().Time;
        int previousScore = levelObject.GetComponent<CompletionStats>().Score;

        // Update the UI text elements
        timeText.text += previousTime.ToString("F2");
        scoreText.text += previousScore.ToString();

        // Access the RectTransform component of the Canvas
        RectTransform canvasRectTransform = GetComponent<RectTransform>();

        // Get the position of the Level game object
        Vector3 levelPosition = levelObject.bounds.center;
        levelPosition.y += levelObject.bounds.size.y * 2;

        // Convert the world position to screen space
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(levelPosition);
        Debug.Log(screenPosition);

        // Set the Canvas position to match the screen position
        canvasRectTransform.position = screenPosition;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levelObject.GetComponent<CompletionStats>().LevelName);
    }
}
