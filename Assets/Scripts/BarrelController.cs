using UnityEngine;
using TMPro;

public class BarrelController : MonoBehaviour
{
    public enum DropType
    {
        WeaponUpgrade,
        SoldierAdd
    }

    [Header("Movement")]
    [SerializeField] private float speed = 2.5f;

    [Header("Durability")]
    [SerializeField] private int maxHp = 3;

    [Header("Display")]
    [SerializeField] private TMP_Text hpText;

    [Header("Drop (即適用)")]
    [SerializeField] private DropType dropType = DropType.WeaponUpgrade;
    [SerializeField] private int soldierAddAmount = 5;

    private int currentHp;
    private bool _isBroken;
    private bool _playerHit;
    private GameManager gameManager;
    private PlayerGroupController playerGroup;

    private void Awake()
    {
        currentHp = maxHp;
        gameManager = FindFirstObjectByType<GameManager>();
        playerGroup = FindFirstObjectByType<PlayerGroupController>();
    }

    private void Start()
    {
        UpdateHpDisplay();
    }

    private void Update()
    {
        if (_isBroken) return;
        if (gameManager != null && !gameManager.IsPlaying) return;

        transform.position += -Vector3.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isBroken) return;

        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Player"))
        {
            HandlePlayerContact(other);
        }
    }

    private void TakeDamage(int amount)
    {
        if (_isBroken) return;

        currentHp -= amount;
        UpdateHpDisplay();
        Debug.Log($"[BarrelController] Damage taken. currentHp={currentHp}");

        if (currentHp <= 0)
        {
            BreakBarrel();
        }
    }

    private void HandlePlayerContact(Collider other)
    {
        if (_playerHit) return;
        _playerHit = true;

        var pg = other.GetComponentInParent<PlayerGroupController>();
        if (pg != null)
        {
            pg.RemoveSoldiers(1);
            Debug.Log("[BarrelController] Player contact: removed 1 soldier.");
        }
    }

    private void BreakBarrel()
    {
        if (_isBroken) return;
        _isBroken = true;

        if (playerGroup != null)
        {
            ApplyDropEffect(playerGroup);
        }
        else
        {
            Debug.LogWarning("[BarrelController] PlayerGroupController not found.");
        }

        Debug.Log($"[BarrelController] Barrel broken. Drop: {dropType}");
        Destroy(gameObject);
    }

    private void ApplyDropEffect(PlayerGroupController pg)
    {
        switch (dropType)
        {
            case DropType.WeaponUpgrade:
                pg.ApplyWeaponUpgrade();
                Debug.Log("[BarrelController] Applied weapon upgrade.");
                break;
            case DropType.SoldierAdd:
                pg.AddSoldiers(soldierAddAmount);
                Debug.Log($"[BarrelController] Added {soldierAddAmount} soldiers.");
                break;
        }
    }

    private void UpdateHpDisplay()
    {
        if (hpText != null)
        {
            string typeLabel = dropType == DropType.WeaponUpgrade ? "W" : "S";
            hpText.text = $"{typeLabel}:{currentHp}";
        }
    }
}
