using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;            // damage of the enemy's attack
    [SerializeField] private float coolDown = 0f;       // time, while enemy can not hit
    [SerializeField] private float attackRange = 1f;    // range of the enemy's attack
    [SerializeField] private LayerMask playerLayer;     // layer containing the player
    [SerializeField] private BoxCollider2D m_HitPosition;   // position of hit_collider
    [SerializeField] private float colliderDistance = 0f;   // distance of hit_collider from the enemy

    private Animator animator;          // reference to the enemy's animator component
    private Health playerHealth;        // reference to the player's Health component
    private float timer = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();    // get enemy's animator Component
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>(); // get player's Health component
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (IsTouchPlayer() && timer >= coolDown)
        {
            Attack();
            timer = 0f;
        }
    }


    private bool IsTouchPlayer()
    {
        Vector2 boxPosition = m_HitPosition.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance;
        Vector2 boxSize = new Vector2(m_HitPosition.bounds.size.x * attackRange, m_HitPosition.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(boxPosition, boxSize, 0, Vector2.left, 0, playerLayer);
        //Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(m_HitPosition.position, attackRange, playerLayer);

        return hit.collider != null;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        playerHealth.TakeDamage(damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(m_HitPosition.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector2(m_HitPosition.bounds.size.x * attackRange, m_HitPosition.bounds.size.y));
    }
}
