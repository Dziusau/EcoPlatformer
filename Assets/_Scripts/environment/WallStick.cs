using UnityEngine;

public class WallStick : MonoBehaviour
{
    [SerializeField] private float stickTime = 1.5f;            // Time player sticks to the wall in seconds
    [SerializeField] private float slideSpeed = 2f;             // Speed at which player slides down the wall
    [SerializeField] private LayerMask m_WhatIsStickingWall;    // Layer mask for walls that player can stick to
    [SerializeField] private Transform m_WallCheck;             // A position marking where to check the wall.
    [SerializeField] private float wallStickDistance = 0.5f;    // Maximum distance player can stick to wall

    private Rigidbody2D rb;
    private float timeSinceStuck;
    private bool isSticking = false;
    private RaycastHit2D hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (IsStickingToWall())
        {
            if (!isSticking) // Player just started sticking to a wall
            {
                isSticking = true;
            }

            if (timeSinceStuck < stickTime)
            {
                timeSinceStuck += Time.fixedDeltaTime;
                rb.velocity = new Vector2(rb.velocity.x, 0f);

                // Calculate the force needed to counteract gravity and mass.
                float gravity = Physics2D.gravity.magnitude * rb.gravityScale;
                Vector2 normal = hit.normal.normalized;
                Vector2 gravityForce = -normal * gravity * rb.mass;

                // Apply the force to the player to stick them to the wall.
                rb.AddForce(gravityForce);
            }
            else
            {
                timeSinceStuck = 0f;
                rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
            }
        }
        else
        {
            if (isSticking) // Player just stopped sticking to a wall
            {
                isSticking = false;
            }

            timeSinceStuck = 0f;
        }
    }

    bool IsStickingToWall()
    {
        if (transform.position.x > 0)
            hit = Physics2D.Raycast(transform.position, transform.right, wallStickDistance, m_WhatIsStickingWall);
        else
            hit = Physics2D.Raycast(transform.position, -transform.right, wallStickDistance, m_WhatIsStickingWall);



        return hit.collider != null;
    }
}