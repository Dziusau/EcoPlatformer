using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private int currentHealth;

    private void Start()
    {
        currentHealth = health;
    }
    public void damagePlayer(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
