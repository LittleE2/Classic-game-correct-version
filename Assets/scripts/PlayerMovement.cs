using UnityEngine;
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




    void Start()
    {

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

        //jump
        jump.action.started += Jump;
        body.freezeRotation = true;

        //close game
        close.action.started += Close;

    }


    //if the jump input is pressed, check if the player is on the ground, if so, jump. 
    private void Jump(InputAction.CallbackContext context)
    {
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



    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundChkPosition.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    //allows team to see the grounded check volume
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(groundChkPosition.position, groundCheckSize);
    //}


}
