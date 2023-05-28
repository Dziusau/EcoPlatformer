using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private float offset = 5f;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            gameManager.PlayerSpawnPoint.position = new Vector3(x: transform.position.x, 
                                                                y: transform.position.y + offset);
    }
}
