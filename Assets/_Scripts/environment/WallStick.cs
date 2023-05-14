using UnityEngine;

public class WallStick : MonoBehaviour
{
    [SerializeField] private float stickTime = 1.5f;            // Time player sticks to the wall in seconds
//    [SerializeField] private float slideSpeed = 2f;             // Speed at which player slides down the wall
    [SerializeField] private LayerMask m_WhatIsStickingWall;    // Layer mask for walls that player can stick to
    [SerializeField] private Transform m_WallCheck;             // A position marking where to check the wall.
    [SerializeField] private float wallStickDistance = 0.2f;    // Maximum distance player can stick to wall

    private Rigidbody2D rb;
    private float timeSinceStuck = 0f;
    private bool isSticking = false;
    private float defaultGravityScale;
    private bool isStickTimeLimit = false;
    
    //private RaycastHit2D hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && !isStickTimeLimit)
        {
            CheckForSticking();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isSticking = false;
            isStickTimeLimit = false;
            rb.gravityScale = defaultGravityScale;
        }
    }

    void FixedUpdate()
    {
        if (isSticking)
        {
            StickToWall();
        } 
    }

    private void CheckForSticking()
    {
        Collider2D[] walls = Physics2D.OverlapCircleAll(m_WallCheck.position, wallStickDistance, m_WhatIsStickingWall);

        foreach (var wall in walls)
        {
            if (wall.gameObject != gameObject)
            {
                isSticking = true;
            }
        }

    }

    private void StickToWall()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        // Increment the stick timer
        timeSinceStuck += Time.fixedDeltaTime;

        if (timeSinceStuck >= stickTime)
        {
            // Player has exceeded the stick time limit, stop sticking and reset the timer
            isSticking = false;
            isStickTimeLimit = true;
            timeSinceStuck = 0f;
            rb.gravityScale = defaultGravityScale;
        }
    }
}