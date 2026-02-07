using UnityEngine;

public class WarningIndicator : MonoBehaviour
{
    [Header("Timing")]
    public float totalDuration = 0.75f;
    public int pulseCount = 2;
    public float fadeOutTime = 0.15f; // smooth exit

    [Header("Opacity")]
    public float minAlpha = 0.25f;
    public float maxAlpha = 0.85f;

    private SpriteRenderer spriteRenderer;
    private float elapsedTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("WarningIndicator requires a SpriteRenderer.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float remainingTime = totalDuration - elapsedTime;

        // Base pulse
        float normalizedTime = Mathf.Clamp01(elapsedTime / totalDuration);
        float pulsePhase = normalizedTime * pulseCount * Mathf.PI * 2f;
        float pulse = Mathf.Sin(pulsePhase) * 0.5f + 0.5f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, pulse);

        // Fade out
        if (remainingTime <= fadeOutTime)
        {
            float fadeT = Mathf.Clamp01(remainingTime / fadeOutTime);
            alpha *= fadeT; // smoothly reduce alpha
        }

        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        // Destroy after totalDuration
        if (elapsedTime >= totalDuration)
        {
            Destroy(gameObject);
        }
    }
}
