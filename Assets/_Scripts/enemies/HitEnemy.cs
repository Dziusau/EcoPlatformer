using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField] private int damage = 1;            // damage of the enemy's attack
    [SerializeField] private float coolDown = 0f;       // time, while enemy can not hit
    [SerializeField] private float attackRange = 1f;    // range of the enemy's attack
    [SerializeField] private LayerMask playerLayer;      // layer containing the player
    [SerializeField] private Transform m_HitPosiotion;  // position of hit_dot

    private Animator animator;          // reference to the enemy's animator component
    private PlayerStats playerStats;    // reference to the player's Stats component
    private float timer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();    // get enemy's animator Component
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); // get player's Stats component
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
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(m_HitPosiotion.position, attackRange, playerLayer);

        return hitPlayer != null;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        playerStats.DamagePlayer(damage);
    }
}
