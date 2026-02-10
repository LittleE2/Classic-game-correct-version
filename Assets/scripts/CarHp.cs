using UnityEngine;

public class CarHp : MonoBehaviour
{
    public int CarMaxHealth = 9;
    private int currentCarHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite2;
    public Sprite sprite3;

    public GameObject pieces;


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

        GameObject fallingOff = Instantiate(pieces, (transform.position + new Vector3(0f, 2.16f, 0)), Quaternion.identity);
        Destroy(fallingOff,1.5f);


        if (currentCarHealth <= 0)
        {
            Debug.Log("car has died");
            Destroy(gameObject);
        }


        if (currentCarHealth == 5)
        {
            ChangeSprite(sprite2);

        }
        if (currentCarHealth == 2)
        {
            ChangeSprite(sprite3);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.CompareTag("playerAttack"))
        {
            TakeDamage(1);
        }
    }

    void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
}
