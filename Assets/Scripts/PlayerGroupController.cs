using UnityEngine;

public class PlayerGroupController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float sideSpeed = 5.0f;

    [Header("X Clamp")]
    [SerializeField] private float minX = -10.0f;
    [SerializeField] private float maxX = 10.0f;

    [Header("References")]
    [SerializeField] private PlayerInputAdapter inputAdapter;
    [SerializeField] private SoldierFormationController formationController;

    [Header("Weapon")]
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private int maxWeaponLevel = 3;

    [Header("Soldier")]
    [SerializeField] private int initialSoldierCount = 1;
    [SerializeField] private int renderCap = 89;

    private int internalCount;
    private GameManager gameManager;

    public int WeaponLevel => weaponLevel;
    public int GetCurrentSoldierCount() => internalCount;
    public int GetRenderCount() => Mathf.Min(internalCount, renderCap);

    public void AddSoldiers(int amount)
    {
        if (amount <= 0) return;
        int old = internalCount;
        internalCount += amount;
        Debug.Log($"[PlayerGroupController] AddSoldiers: {old} -> {internalCount}");
        OnSoldierCountChanged();
    }

    public void RemoveSoldiers(int amount)
    {
        if (amount <= 0) return;
        if (internalCount <= 0) return;

        int old = internalCount;
        internalCount = Mathf.Max(0, internalCount - amount);
        Debug.Log($"[PlayerGroupController] RemoveSoldiers: {old} -> {internalCount}");
        OnSoldierCountChanged();

        if (internalCount <= 0)
        {
            Debug.Log("[PlayerGroupController] All soldiers lost. GameOver.");
            if (gameManager != null)
            {
                gameManager.SetGameOver();
            }
        }
    }

    /// <summary>
    /// トゲ接触用: 外周の兵士1体にダメージを与える。HP=0で死亡→人数減少。
    /// </summary>
    public void DamageOneSoldier()
    {
        if (formationController == null) return;

        var target = formationController.FindDamageTarget();
        if (target != null)
        {
            target.TakeDamage(1);
            if (!target.IsAlive())
            {
                formationController.RemoveDeadSoldier(target);
                RemoveSoldiers(1);
            }
        }
    }

    public void ApplyWeaponUpgrade(int levelDelta = 1)
    {
        int oldLevel = weaponLevel;
        weaponLevel = Mathf.Clamp(weaponLevel + levelDelta, 1, maxWeaponLevel);
        Debug.Log($"[PlayerGroupController] Weapon level: {oldLevel} -> {weaponLevel}");
    }

    private void OnSoldierCountChanged()
    {
        if (formationController != null)
        {
            formationController.RebuildFormation(GetRenderCount());
        }
    }

    private void Awake()
    {
        if (inputAdapter == null)
        {
            inputAdapter = GetComponent<PlayerInputAdapter>();
        }

        internalCount = initialSoldierCount;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        OnSoldierCountChanged();
    }

    private void Update()
    {
#if UNITY_EDITOR
        // デバッグ用: Qキーで+1、Eキーで-1
        if (UnityEngine.InputSystem.Keyboard.current != null)
        {
            if (UnityEngine.InputSystem.Keyboard.current.qKey.wasPressedThisFrame)
                AddSoldiers(1);
            if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
                RemoveSoldiers(1);
        }
#endif

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
