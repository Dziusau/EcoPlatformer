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

    public int CurrentHealth { get => currentHealth; }

    private void Awake()
    {
        currentHealth = health;
        score = 0;
    }

    public void DamagePlayer(int _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, health);
        if (currentHealth > 0)
        {
            Debug.Log(currentHealth);
        }
        else
        {
            OnDieEvent.Invoke();
        }
    }

    public void HillPlayer(int _health) => currentHealth += _health;

    public void AddScore(int _score) => this.score += _score;
}
