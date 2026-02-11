using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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

    // ===== ADDED (Game Over) =====
    public LevelLoader levelLoader;
    public int gameOverSceneIndex = 3;
    private bool isDead = false;
    // =============================

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

    //void Awake()
    //{
    //    jump = playerActionMap.FindAction("Jump");
    //    movement = playerActionMap.FindAction("Movement");
    //}

    //private void OnEnable()
    //{
    //    //jump.action.started += Jump;
    //    Jump.performed += jump;
    //    jump.canceled += Jump;
    //    jump.Enable();

    //}

    //private void OnDisable()
    //{
    //    //jump.action.canceled -= Jump;

    //}

    void Update()
    {
        //Debug.Log(moveDirection.x);
       // if (isDead) return;

        movement();
    }

    //handles player movement
    void movement()
    {
        //left to right movement, reads the input and passes it into move direction which determines left or right, and multiplies by move speed. 
        moveDirection = move.action.ReadValue<Vector2>();
        body.linearVelocityX = moveDirection.x * moveSpeed;

        //jump
        jump.action.started += Jump;
        body.freezeRotation = true;

        //close game
        close.action.started += Close;
    }

    //if the jump input is pressed, check if the player is on the ground, if so, jump. 
    private void Jump(InputAction.CallbackContext context)
    {
        //if (isDead) return;

        if (isGrounded())
        {
            body.linearVelocity = Vector2.up * jumpStrength;
        }
    }

    private void Close(InputAction.CallbackContext context)
    {
        Debug.Log("close game");
        Application.Quit();
    }

    //checks if player is grounded
    private bool isGrounded()
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
        //Debug.Log("hit");
        if (isDead) return;

        if (other.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }

    //player health
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        Debug.Log("Player has taken damage");

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0 && !isDead)
        {
            //player death, set up game over screen. 
            Debug.Log("player has died");
            isDead = true;

            body.linearVelocity = Vector2.zero;

            if (levelLoader != null)
            {
                levelLoader.LoadLevelByIndex(gameOverSceneIndex);
            }
            else
            {
                Debug.LogError("LevelLoader not assigned on PlayerMovement");
            }
        }
    }




    //void Awake()
    //{
    //    jump.performed
    //}

    //private void OnEnable()
    //{
        
    //}

    //private void OnDisable()
    //{
        
    //}

    //allows team to see the grounded check volume
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(groundChkPosition.position, groundCheckSize);
    //}
}
