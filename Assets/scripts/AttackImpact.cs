using UnityEngine;

public class AttackImpact : MonoBehaviour
{
    void Start()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //checks if hits the car, prints statements, deletes immediatly. 
        if (other.CompareTag("Car"))
        {
           // Debug.Log("attack hit something: " + other.gameObject.name);
            Destroy(gameObject);
        }
        //destroys after 1 second
        Destroy(gameObject, 1f);
    }
}
