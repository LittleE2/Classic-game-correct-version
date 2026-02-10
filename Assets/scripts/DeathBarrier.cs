using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            KillPlayer(player);
        }
    }

    void KillPlayer(PlayerMovement player)
    {
        Debug.Log("Player entered death barrier");

        // Deal lethal damage using existing health system
        player.TakeDamage(player.maxHealth);
    }
}
