using UnityEngine;

public class PlayerGroupController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float sideSpeed = 5.0f;

    [Header("X Clamp")]
    [SerializeField] private float minX = -5.0f;
    [SerializeField] private float maxX = 5.0f;

    [Header("References")]
    [SerializeField] private PlayerInputAdapter inputAdapter;

    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private int maxWeaponLevel = 5;

    [Header("Temporary HP")]
    [SerializeField] private int maxHp = 2;

    private int currentHp;
    private GameManager gameManager;

    public int WeaponLevel => weaponLevel;
    public int CurrentHp => currentHp;

    public void UpgradeWeaponLevel()
    {
        int oldLevel = weaponLevel;
        weaponLevel = Mathf.Clamp(weaponLevel + 1, 1, maxWeaponLevel);
        Debug.Log($"[PlayerGroupController] Weapon level up: {oldLevel} -> {weaponLevel}");
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        int oldHp = currentHp;
        currentHp = Mathf.Min(currentHp + amount, maxHp);
        Debug.Log($"[PlayerGroupController] Healed: {oldHp} -> {currentHp}");
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        if (currentHp <= 0) return;

        int oldHp = currentHp;
        currentHp = Mathf.Max(0, currentHp - amount);

        Debug.Log($"[PlayerGroupController] Damage taken: {oldHp} -> {currentHp}");

        if (currentHp <= 0)
        {
            Debug.Log("[PlayerGroupController] Player group HP reached 0. GameOver.");
            if (gameManager != null)
            {
                gameManager.SetGameOver();
            }
        }
    }
    private void Awake()
    {
        if (inputAdapter == null)
        {
            inputAdapter = GetComponent<PlayerInputAdapter>();
        }

        currentHp = maxHp;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        Vector3 pos = transform.position;

        float xInput = 0f;
        bool isPointerInput = false;

        if (inputAdapter != null)
        {
            xInput = inputAdapter.CurrentHorizontal;
            isPointerInput = inputAdapter.IsPointerInput;
        }

        if (isPointerInput)
        {
            pos.x += xInput * sideSpeed;
        }
        else
        {
            pos.x += xInput * sideSpeed * dt;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}
