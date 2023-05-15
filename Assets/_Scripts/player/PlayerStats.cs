using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 3;
    public int score;

    [Header("Events")]
    [Space]

    public UnityEvent OnDieEvent;

    private int currentHealth;

    private void Start()
    {
        currentHealth = health;
        score = 0;
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            OnDieEvent.Invoke();
        }

        Debug.Log(currentHealth);
    }

    public void HillPlayer(int health) => currentHealth += health;

    public void AddScore(int score) => this.score += score;
}
