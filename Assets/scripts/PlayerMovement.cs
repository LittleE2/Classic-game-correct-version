using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    //variables
    //movement modfiers
    public float moveSpeed;
    public Rigidbody2D body;
    private Vector2 moveDirection;
    //player input
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference close;
    public float jumpStrength = 15f;

    //ground check variables
    public Transform groundChkPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    //health variables
    public int maxHealth = 4;
    private int currentHealth;

    public Slider healthSlider;


    void Start()
    {
        // Set starting health
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        movement();

    }



    //handles player movement
    void movement()
    {
        //left to right movement, reads the input and passes it into move direction which determines left or right, and multiplies by move speed. 
        moveDirection = move.action.ReadValue<Vector2>();
        body.linearVelocityX = moveDirection.x * moveSpeed;
        if (body.linearVelocityX != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        //jump
        jump.action.started += Jump;
        body.freezeRotation = true;
        //close game
        close.action.started += Close;
    }

    //if the jump input is pressed, check if the player is on the ground, if so, jump. 
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            body.linearVelocity = Vector2.up * jumpStrength;
        }
    }

    
    
    public void Close(InputAction.CallbackContext context)
    {
        Debug.Log("close game");
        Application.Quit();
    }


    //checks if player is grounded
    public bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundChkPosition.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
            return false;
    }

    //activates if player enters specific triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }


    //player health
    private void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log("Player has taken damage");

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Debug.Log("player has died");
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
