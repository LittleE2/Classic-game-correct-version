using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class playerAttack : MonoBehaviour
{

    public InputActionReference Punch;
    public InputActionReference Kick;
    public InputActionReference move;
    public GameObject punchingPrefab;
    public GameObject kickingPrefab;
    private Vector2 moveDirection;

    //checks for certain 
    private float directionChange;
    private float attackDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
        directionChange = moveDirection.x;

        if (directionChange != 0)
        {
            attackDirection = directionChange;

        }
        Debug.Log(attackDirection);
        Punch.action.started += fAttack;
        Kick.action.started += vAttack;
    }


    private void fAttack(InputAction.CallbackContext context)
    {

        Debug.Log("Punch!");
    }

    private void vAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Kick!");
    }
}
