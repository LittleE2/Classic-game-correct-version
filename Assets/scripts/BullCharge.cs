using UnityEngine;

public class BullCharge : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 12f;

    [Header("Despawn")]
    public float despawnX = 20f;

    //  1 = left ? right
    // -1 = right ? left
    private int direction = 1;

    /// <summary>
    /// Called by the spawner to set which way the bull runs.
    /// </summary>
    public void SetDirection(int newDirection)
    {
        direction = Mathf.Clamp(newDirection, -1, 1);

        // Flip the sprite visually
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void Update()
    {
        // Move horizontally every frame
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Despawn when far off-screen
        if (direction == 1 && transform.position.x > despawnX)
            Destroy(gameObject);

        if (direction == -1 && transform.position.x < -despawnX)
            Destroy(gameObject);
    }
}
