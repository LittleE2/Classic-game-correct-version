using UnityEngine;

public class AvoidedProgression : MonoBehaviour
{
    [Header("Progression")]
    public int bullsToAvoid = 10;

    private int bullsAvoided;

    private LevelLoader levelLoader;

    void Awake()
    {
        levelLoader = FindFirstObjectByType<LevelLoader>();

        if (levelLoader == null)
        {
            Debug.LogError("No LevelLoader found in scene!");
        }
    }

    public void RegisterBullAvoided()
    {
        bullsAvoided++;

        Debug.Log($"Bulls avoided: {bullsAvoided}/{bullsToAvoid}");

        if (bullsAvoided >= bullsToAvoid)
        {
            levelLoader.LoadNextLevel();
        }
    }
}
