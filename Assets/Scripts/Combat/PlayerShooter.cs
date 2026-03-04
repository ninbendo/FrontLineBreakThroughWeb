using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerGroupController playerGroup;

    [Header("Tuning")]
    [SerializeField] private float fireInterval = 1.0f; // 1秒に4発なら0.25fとする

    private GameManager gameManager;
    private float _timer;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void Awake()
    {
        if (playerGroup == null)
        {
            playerGroup = GetComponent<PlayerGroupController>();
        }
    }

    private void Update()
    {
        if (gameManager != null && !gameManager.IsPlaying)
        {
            return;
        }

        if (bulletPrefab == null || muzzle == null) return;

        _timer += Time.deltaTime;

        // 1フレームで複数発ぶん溜まっていたら追いつく（低FPS/ヒッチ対策）
        int safety = 0;
        const int maxShotsPerFrame = 5; // ヒッチ時の弾幕暴発を防ぐ上限（3〜5推奨）

        float currentInterval = GetCurrentFireInterval();

        while (_timer >= currentInterval && safety < maxShotsPerFrame)
        {
            _timer -= currentInterval;
            Shoot();
            safety++;
        }
    }

    private float GetCurrentFireInterval()
    {
        if (playerGroup == null)
        {
            return fireInterval;
        }

        switch (playerGroup.WeaponLevel)
        {
            case 1: return fireInterval;         // 1秒に1発
            case 2: return fireInterval / 2.0f;  // 1秒に2発
            case 3: return fireInterval / 3.0f;  // 1秒に3発
            case 4: return fireInterval / 4.0f;  // 1秒に4発
            default: return fireInterval / 5.0f; // 1秒に5発
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
    }
}

