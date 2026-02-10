using UnityEngine;

public class CarHp : MonoBehaviour
{
    public int CarMaxHealth = 10;
    private int currentCarHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCarHealth = CarMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageTaken)
    {
        currentCarHealth -= damageTaken;
        Debug.Log("Current car hp: " + currentCarHealth);


        if (currentCarHealth <= 0)
        {
            Debug.Log("car has died");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hit");
        if (other.CompareTag("playerAttack"))
        {
            TakeDamage(1);
        }
    }
}
