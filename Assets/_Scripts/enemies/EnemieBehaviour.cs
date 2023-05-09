using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackDelay = 1f;
    //[SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private PlayerStats playerStats;
    private GameObject target;
    private bool isMoving = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerStats = target.GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            // Move towards the player if they are within range.
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= attackRange)
            {
                // If the player is within attack range, stop moving and attack.
                isMoving = false;
                if (!isAttacking && Time.time > lastAttackTime + attackDelay)
                {
                    Attack();
                }
            }
            else
            {
                // If the player is outside attack range, move towards the player.
                isMoving = true;
                isAttacking = false;
                Vector2 direction = target.transform.position - transform.position;
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }
        }

        // Update the animator parameters.
//        animator.SetBool("isMoving", isMoving);
//        animator.SetBool("attack", isAttacking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.deltaTime;

        playerStats.damagePlayer(attackDamage);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a circle to show the attack range.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
