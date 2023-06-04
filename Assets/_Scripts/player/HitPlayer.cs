using UnityEngine;

public class HitPlayer : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;      // damage of the player's attack
    [SerializeField] private LayerMask enemyLayer;      // layer containing the enemies
    [SerializeField] private Transform m_HitPosition;   // position of hit_dot
    [SerializeField] private float coolDown = 0f;       // time, while player can not hit
    [SerializeField] private float attackRange = 1f;    // range of the player's attack
    [SerializeField] private float colliderDistance = 0f;   // distance of hit_collider from the player

    private Animator animator;  // reference to the player's animator component
    private float timer = 0f;
    private bool isBlock = false;

    public bool IsBlock { get => isBlock; }

    private void Awake()
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

        if (Input.GetKey(KeyCode.G))
        {
            isBlock = true;
            animator.SetBool("Block", true);
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            isBlock = false;
            animator.SetBool("Block", false);
        }
    }

    private void Attack()
    {
        // Set the attack animation trigger
        animator.SetTrigger("Hit");

        // Detect enemies in range
        Vector3 direction = transform.right * transform.localScale.x;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(point: m_HitPosition.position + colliderDistance * direction.normalized, 
                                                             radius: attackRange, 
                                                             layerMask: enemyLayer);

        // Deal damage to hit enemies
        foreach (Collider2D enemy in hitEnemies)
            enemy.GetComponent<Health>().TakeDamage(attackDamage);        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.right * transform.localScale.x;
        Gizmos.DrawWireSphere(m_HitPosition.position + colliderDistance * direction.normalized, attackRange);
    }
}
