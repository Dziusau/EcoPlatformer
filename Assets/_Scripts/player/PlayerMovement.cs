using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float moveSpeed = 10f; // the speed at which the player moves

    [Header ("Collecting")]
    [SerializeField] private LayerMask collectiblesLayer;
    [SerializeField] private float collectRange = 2f;
    
    private PlayerController playerController;
    private GameManager gameManager;
    private Animator animator;
    private float horizontalMovement;
    private bool isJump = false;
    private bool isCrouch = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            Collect();
        }

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
