using UnityEngine;

public sealed class SpikeController : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager == null || !gameManager.IsPlaying) return;
        if (!other.CompareTag("Player")) return;

        var playerGroup = other.GetComponentInParent<PlayerGroupController>();
        if (playerGroup != null)
        {
            playerGroup.DamageOneSoldier();
            Debug.Log("[SpikeController] Damaged one soldier.");
        }
    }
}
