using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private int currentHealth;


    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberFlashes;
    [SerializeField] private Color iFrameColor;
    [SerializeField] private LayerMask enemyLayer;

    private int playerLayerID;
    private int enemyLayerID;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private GameManager gameManager;

    public int CurrentHealth { get => currentHealth; }


    private void Awake()
    {
        currentHealth = health;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        GetSpriteRenderers();

        playerLayerID = ConvertLayerMaskToLayerID(LayerMask.GetMask("Player"));
        enemyLayerID = ConvertLayerMaskToLayerID(enemyLayer);
    }


    public void TakeDamage(int _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, health);
        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (gameObject.CompareTag("Player"))
            {
                gameManager.PlayerDied(gameObject);
            }
            else
            {
                gameManager.EnemyDied(gameObject);
            }
        }
    }

    public void Hill(int _health) => currentHealth = Mathf.Clamp(currentHealth + _health, 0, health);

    private IEnumerator Invulnerability()
    { 
        Physics2D.IgnoreLayerCollision(playerLayerID, enemyLayerID, true);
        for (int i = 0; i < numberFlashes; i++)
        {
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.color = iFrameColor;
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2));
            
            foreach (var spriteRenderer in spriteRenderers)
                spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(playerLayerID, enemyLayerID, false);
    }

    public int ConvertLayerMaskToLayerID(LayerMask layerMask)
    {
        int layerID = 0;

        for (int i = 0; i < 32; i++)
        {
            if ((layerMask.value & (1 << i)) != 0)
            {
                layerID = i;
                break;
            }
        }

        return layerID;
    }

    private void GetSpriteRenderers()
    {
        SpriteRenderer parentSpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponentsInChildren<SpriteRenderer>(spriteRenderers);

        if (parentSpriteRenderer != null)
            spriteRenderers.Add(parentSpriteRenderer);
    }
}
