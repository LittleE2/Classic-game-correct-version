using UnityEngine;

public class CarHp : MonoBehaviour
{
    public int CarMaxHealth = 15;
    private int currentCarHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite2;
    public Sprite sprite3;

    public GameObject pieces;

    // Win Screen
    public LevelLoader levelLoader;
    public int winSceneIndex = 4; // set this to your Win scene index
    private bool hasWon = false;

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

        GameObject fallingOff = Instantiate(pieces, (transform.position + new Vector3(0f, 2f, 0)), Quaternion.identity);
        Destroy(fallingOff, 1.5f);

        if (currentCarHealth <= 0 && !hasWon)
        {
            //car has died, instert win screen. 
            Debug.Log("car has died");
            hasWon = true;

            if (levelLoader != null)
            {
                levelLoader.LoadLevelByIndex(winSceneIndex);
            }
            else
            {
                Debug.LogError("LevelLoader not assigned on CarHp");
            }

            Destroy(gameObject);
        }

        if (currentCarHealth == 10)
        {
            ChangeSprite(sprite2);
        }

        if (currentCarHealth == 5)
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
