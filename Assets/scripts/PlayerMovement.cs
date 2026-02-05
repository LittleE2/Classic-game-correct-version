using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //variables
    public float moveSpeed;
    public Rigidbody2D body;
    private Vector2 moveDirection;
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference close;
    public float jumpStrength = 15f;



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
    void playerJumping()
    {
        Debug.Log("jump");
        body.linearVelocity = Vector2.up * jumpStrength;
    }

    //currently tests, runs when the buttons are pressed. 
    private void Jump(InputAction.CallbackContext context)
    {

        playerJumping();

    }

    private void Close(InputAction.CallbackContext context)
    {
        Debug.Log("close game");
    }



}
