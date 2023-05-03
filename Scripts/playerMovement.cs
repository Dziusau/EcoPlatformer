using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // the speed at which the player moves
    
    private playerController playerController;
    private Animator animator;
    private float horizontalMovement;
    private bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<playerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed; // get the horizontal input axis (left/right arrow keys)

        if (Input.GetButtonDown("Jump")) // if the player presses the spacebar
        {
            isJump = true;
        }

        // update the animator's parameters based on the player's movement and jump state
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        //animator.SetBool("IsJumping", jumpState == JumpState.Jumping);
        //animator.SetBool("IsFalling", jumpState == JumpState.Falling);
    }

    void FixedUpdate()
    {
        playerController.Move(horizontalMovement * Time.fixedDeltaTime, false, isJump);
        isJump = false;
    }
}
