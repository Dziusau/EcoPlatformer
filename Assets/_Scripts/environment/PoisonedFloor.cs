using UnityEngine;

public class PoisonedFloor : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageDelay = 1f;
    [SerializeField] private LayerMask m_WhatIsPlayer;

    private Health playerHealth;    // reference to the player's Health component
    private float timer;
    private Collider2D collider;

    // Start is called before the first frame update
    private void Awake()
    {
        timer = 0f;
        collider = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (IsOnPoisonedGroung() && timer >= damageDelay)
        {
            playerHealth.TakeDamage(damage);
            timer = 0f;
        }
    }

    private bool IsOnPoisonedGroung()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.up, 0.1f, m_WhatIsPlayer);
        if (hit)
        {
            playerHealth = hit.transform.gameObject.GetComponent<Health>();
            return true;
        }
        return false;
    }
}
