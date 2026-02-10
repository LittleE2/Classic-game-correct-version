using UnityEngine;

public class AttackImpact : MonoBehaviour
{
    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Punch hit something: " + other.gameObject.name);

            Debug.Log("hitcar");
        }
    }
}
