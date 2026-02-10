using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float moveSpeed;
    public Rigidbody2D body;
    private Vector2 moveDirection;

    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference close;
    public float jumpStrength = 15f;

    public Transform groundChkPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    public int maxHealth = 4;
    private int currentHealth;
    public Slider healthSlider;

    //jumping anim tools (working)
    private bool isJumping;
    private bool wasGrounded;

    //sprite direction change (test)
    private float XPosLastFrame;
    [SerializeField] private SpriteRenderer spriteRenderer;


    void Start()
    {   
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
    void OnEnable()
    {
        jump.action.started += Jump;
        close.action.started += Close;
    }

    void OnDisable()
    {
        jump.action.started -= Jump;
        close.action.started -= Close;
    }

    void Update()
    {
        Movement();
        FlipCharacterX();
        bool grounded = isGrounded();

        // just left the ground
        if (wasGrounded && !grounded)
        {
            isJumping = true;
        }
        // just landed
        if (!wasGrounded && grounded)
        {
            isJumping = false;
        }

        animator.SetBool("isJumping", isJumping);

        wasGrounded = grounded;
    }
        void Movement()
    {
        moveDirection = move.action.ReadValue<Vector2>();
        body.linearVelocity = new Vector2(moveDirection.x * moveSpeed, body.linearVelocity.y);

        animator.SetBool("isRunning", moveDirection.x != 0);
        animator.SetBool("isRunning", moveDirection.x != 0 && !isJumping);
    }
    private void FlipCharacterX ()
    {
    if (transform.position.x > XPosLastFrame)
        {
            spriteRenderer.flipX = false;
        }
    else if (transform.position.x < XPosLastFrame)
        {
            spriteRenderer.flipX= true;
        }
        XPosLastFrame = transform.position.x;

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpStrength);
            isJumping = true;
        }
    }

    public void Close(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapBox(
            groundChkPosition.position,
            groundCheckSize,
            0,
            groundLayer
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}




//allows team to see the grounded check volume
//private void OnDrawGizmosSelected()
//{
//    Gizmos.color = Color.yellow;
//    Gizmos.DrawWireCube(groundChkPosition.position, groundCheckSize);
//}
