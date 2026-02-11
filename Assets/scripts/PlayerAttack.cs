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

    //animssecondround
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //finds the last move direction of the player (is the player looking right or left)
        moveDirection = move.action.ReadValue<Vector2>();
        directionChange = moveDirection.x;

        if (directionChange != 0)
        {
            attackDirection = directionChange;

        }
        //Debug.Log(attackDirection);





        Punch.action.started += fAttack;
        Kick.action.started += vAttack;
    }

    public void FinishPunching()
    {
        animator.SetBool("isPunching", false);
    }
    public void FinishKicking()
    {
        animator.SetBool("isKicking", false);
    }
    private void fAttack(InputAction.CallbackContext context)
    {
        GameObject punch = Instantiate(punchingPrefab, (transform.position + new Vector3(attackDirection, 1,0)), Quaternion.identity);
        animator.SetBool("isPunching", true);
        //Debug.Log("Punch!");
    }

    private void vAttack(InputAction.CallbackContext context)
    {
        GameObject kick = Instantiate(kickingPrefab, (transform.position + new Vector3(attackDirection, -1, 0)), Quaternion.identity);
        animator.SetBool("isKicking", true);
        //Debug.Log("Kick!");
    }
}
