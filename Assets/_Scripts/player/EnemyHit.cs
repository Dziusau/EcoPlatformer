using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f; // range of the player's attack
    [SerializeField] private int attackDamage = 1; // damage of the player's attack
    [SerializeField] private LayerMask enemyLayer; // layer containing the enemies
    [SerializeField] private Transform m_HitPosiotion;
    [SerializeField] private float coolDown = 0f;

    private Animator animator; // reference to the player's animator component
    private float timer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && timer >= coolDown)
        {
            Attack();
            timer = 0f;
        }
    }

    private void Attack()
    {
        // Set the attack animation trigger
        //animator.SetTrigger("attack");

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(m_HitPosiotion.position, attackRange, enemyLayer);

        // Deal damage to hit enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit_player");
            //enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack range gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
