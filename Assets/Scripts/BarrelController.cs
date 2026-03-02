using UnityEngine;

public class BarrelController : MonoBehaviour
{
    [Header("Durability")]
    [SerializeField] private int maxHp = 3;

    [Header("Drop")]
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawnPoint;

    private int currentHp;
    private bool isBroken = false;

    private void Awake()
    {
        currentHp = maxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            HandlePlayerHit();
            BreakBarrel();
        }
    }

    private void TakeDamage(int amount)
    {
        if (isBroken) return;

        currentHp -= amount;
        Debug.Log($"[BarrelController] Damage taken. currentHp={currentHp}");

        if (currentHp <= 0)
        {
            BreakBarrel();
        }
    }

    private void HandlePlayerHit()
    {
        // Day6では仮実装
        Debug.Log("[BarrelController] Player hit barrel: decrease player count by 1");
    }

    private void BreakBarrel()
    {
        if (isBroken) return;
        isBroken = true;

        Debug.Log("[BarrelController] Barrel broken.");

        if (dropPrefab != null)
        {
            Vector3 spawnPos = dropSpawnPoint != null
                ? dropSpawnPoint.position
                : transform.position;

            Instantiate(dropPrefab, spawnPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
