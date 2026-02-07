using UnityEngine;
using System.Collections;

public class BullSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bullPrefab;
    public GameObject warningPrefab; // optional

    [Header("Timing")]
    public float spawnInterval = 4f;
    public float warningDuration = 0.75f;

    [Header("Ground Plane")]
    public float groundY = -2.5f;

    [Header("Spawn Positions")]
    public float leftSpawnX = -15f;
    public float rightSpawnX = 15f;

    private float timer;

    // Anti-streak memory
    private int lastDirection = 0;     // 1 or -1
    private int sameDirectionCount = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            StartCoroutine(SpawnSequence());
        }
    }

    IEnumerator SpawnSequence()
    {
        int direction = ChooseDirection();

        // Optional warning
        if (warningPrefab != null)
            SpawnWarning(direction);

        // Wait before bull appears
        yield return new WaitForSeconds(warningDuration);

        SpawnBull(direction);
    }

    void SpawnBull(int direction)
    {
        float spawnX = direction == 1 ? leftSpawnX : rightSpawnX;

        GameObject bull = Instantiate(bullPrefab);

        // Set movement direction
        BullCharge charge = bull.GetComponent<BullCharge>();
        if (charge != null)
            charge.SetDirection(direction);

        // Feet anchoring
        Transform feet = bull.transform.Find("Feet");
        if (feet == null)
        {
            Debug.LogError("Bull prefab is missing a Feet child object.");
            Destroy(bull);
            return;
        }

        float offset = bull.transform.position.y - feet.position.y;

        bull.transform.position = new Vector3(
            spawnX,
            groundY + offset,
            0f
        );
    }

    void SpawnWarning(int direction)
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        // Screen edge placement
        float viewportX = direction == 1 ? 0.05f : 0.95f;

        Vector3 worldPos = cam.ViewportToWorldPoint(
            new Vector3(viewportX, 0.5f, cam.nearClipPlane)
        );

        worldPos.z = 0f;
        Instantiate(warningPrefab, worldPos, Quaternion.identity);
    }

    int ChooseDirection()
    {
        float leftChance = 0.5f;

        // Bias against streaks
        if (sameDirectionCount >= 2)
        {
            if (lastDirection == 1) leftChance = 0.25f;
            if (lastDirection == -1) leftChance = 0.75f;
        }

        int newDirection = Random.value < leftChance ? 1 : -1;

        if (newDirection == lastDirection)
            sameDirectionCount++;
        else
            sameDirectionCount = 1;

        lastDirection = newDirection;
        return newDirection;
    }
}
