using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private Transform target;
    private bool isMoving = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            // Move towards the player if they are within range.
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
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
                Vector2 direction = target.position - transform.position;
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }
        }

        // Update the animator parameters.
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("attack", isAttacking);
    }

    void Attack()
    {
        // Check if the player is within attack range.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);
        
        isAttacking = true;
        lastAttackTime = Time.time;

        //foreach (Collider2D collider in colliders)
        //{
        //    // Deal damage to the player.
        //    PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
        //    if (playerHealth != null)
        //    {
        //        playerHealth.TakeDamage(attackDamage);
        //    }
        //}
    }

    void OnDrawGizmosSelected()
    {
        // Draw a circle to show the attack range.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
