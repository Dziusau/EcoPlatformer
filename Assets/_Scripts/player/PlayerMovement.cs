using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // the speed at which the player moves
    
    private PlayerController playerController;
    private Animator animator;
    private float horizontalMovement;
    private bool isJump = false;
    private bool isCrouch = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed; // get the horizontal input axis (left/right arrow keys)

        if (Input.GetButtonDown("Jump")) // if the player presses the spacebar
        { 
            isJump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
            isCrouch = true;
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;

        // update the animator's parameters based on the player's movement and jump state
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        playerController.Move(horizontalMovement * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}
