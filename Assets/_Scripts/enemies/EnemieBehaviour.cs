using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Animator animator;
    private GameObject target;
    private bool isMoving = false;
    private bool m_FacingRight = true;  // For determining which way the enemy is currently facing.

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            // If the player is outside attack range, move towards the player.
            Vector2 direction = target.transform.position - transform.position;
            if(direction != Vector2.zero)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            transform.Translate(speed * Time.deltaTime * direction.normalized);

            if(direction.x > 0 && m_FacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && !m_FacingRight)
            {
                Flip();
            }
        }

        //Update the animator parameters.
        animator.SetBool("isMoving", isMoving);        
    }

    private void Flip()
    {
        // Switch the way the enemy is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the enemy's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
