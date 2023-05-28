using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float moveSpeed = 10f; // the speed at which the player moves

    [Header ("Collecting")]
    [SerializeField] private LayerMask collectiblesLayer;
    [SerializeField] private float collectRange = 2f;

    private enum MovementState { idle, running, jumping, falling };
    
    private PlayerController playerController;
    private GameManager gameManager;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private float horizontalMovement;
    private bool isJump;
    private bool isCrouch;
    private MovementState state;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isJump = false;
        isCrouch = false;   
        state = MovementState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed; // get the horizontal input axis (left/right arrow keys)

        if (Input.GetButtonDown("Jump")) // if the player presses the spacebar
            isJump = true;

        if (Input.GetButtonDown("Crouch"))
            isCrouch = true;
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;

        if (Input.GetKeyDown(KeyCode.D))
        {
            Collect();
        }

        // update the animator's parameters based on the player's movement and jump state
        UpdateAnimatorState();
    }

    public void OnLanding()
    {
        Debug.Log("Grounded");
       // animator.SetBool("IsJumping", false);
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

    private void UpdateAnimatorState() 
    {
        state = MovementState.idle;
        
        if (horizontalMovement != 0f)
            state = MovementState.running;

        if (rigidbody.velocity.y > 0.1f)
            state = MovementState.jumping;
        else if(rigidbody.velocity.y < -0.1f)
            state = MovementState.falling;

        animator.SetInteger("State", (int) state);
    }

    private void Collect()
    {
        Collider2D[] heaps = Physics2D.OverlapCircleAll(transform.position, collectRange, collectiblesLayer);

        foreach (var heap in heaps)
        {
            heap.gameObject.SetActive(false);
            gameManager.AddScore(10);
        }
    }
}
