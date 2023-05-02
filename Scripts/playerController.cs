using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // the speed at which the player moves
    [SerializeField] private float jumpForce = 10f; // the force with which the player jumps

    private Rigidbody2D rigidbody;
    private Animator animator;
    private float horizontalMovement;
    private JumpState jumpState;

    public enum JumpState
    {
        Grounded,
        Jumping,
        Falling
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        jumpState = JumpState.Grounded;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        //if (Input.GetKeyDown(KeyCode.Space))
        //    rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);

        horizontalMovement = Input.GetAxisRaw("Horizontal"); // get the horizontal input axis (left/right arrow keys)

        if (Input.GetButtonDown("Jump") && jumpState == JumpState.Grounded) // if the player presses the spacebar and is on the ground
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // make the player jump
            jumpState = JumpState.Jumping; // set the jump state to Jumping
        }

        // update the animator's parameters based on the player's movement and jump state
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        //animator.SetBool("IsJumping", jumpState == JumpState.Jumping);
        //animator.SetBool("IsFalling", jumpState == JumpState.Falling);
    }

    void FixedUpdate()
    {
        float verticalVelocity = rigidbody.velocity.y;

        if (jumpState == JumpState.Jumping && verticalVelocity <= 0f) // if the player has finished jumping and is now falling
        {
            jumpState = JumpState.Falling; // set the jump state to Falling
        }
        else if (jumpState == JumpState.Falling && IsGrounded()) // if the player has landed on the ground after falling
        {
            jumpState = JumpState.Grounded; // set the jump state back to Grounded
        }

        // move the player horizontally based on the input axis
        rigidbody.velocity = new Vector2(horizontalMovement * moveSpeed, verticalVelocity);

        // save the base scale value of the player
        Vector3 baseScale = transform.localScale;

        // flip the player sprite if they're moving left
        if (horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
        // flip the player sprite back if they're moving right
        else if (horizontalMovement > 0f)
        {
            transform.localScale = baseScale;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        return hit.collider != null;
    }
}
