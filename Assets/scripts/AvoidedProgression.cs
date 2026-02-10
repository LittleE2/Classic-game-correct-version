using UnityEngine;
using TMPro;

public class AvoidedProgression : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI progressText;

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

        UpdateUI();
    }

    public void RegisterBullAvoided()
    {
        bullsAvoided++;
        UpdateUI();

        Debug.Log($"Bulls avoided: {bullsAvoided}/{bullsToAvoid}");

        if (bullsAvoided >= bullsToAvoid)
        {
            levelLoader.LoadNextLevel();
        }
    }

    void UpdateUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Bulls Avoided: {bullsAvoided} / {bullsToAvoid}";
        }
    }
}
