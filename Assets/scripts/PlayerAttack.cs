using UnityEngine;
using UnityEngine.InputSystem;

public class playerAttack : MonoBehaviour
{

    public InputActionReference Punch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Punch.action.started += fAttack;
    }


    private void fAttack(InputAction.CallbackContext context)
    {
        Debug.Log("attack!");
    }
}
