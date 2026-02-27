using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Tuning")]
    [SerializeField] private float fireInterval = 0.25f;

    private float _timer;

    private void Update()
    {
        if (bulletPrefab == null || muzzle == null) return;

        _timer += Time.deltaTime;

        // 1フレームで複数発ぶん溜まっていたら追いつく（低FPS/ヒッチ対策）
        int safety = 0;
        const int maxShotsPerFrame = 5; // ヒッチ時の弾幕暴発を防ぐ上限（3〜5推奨）

        while (_timer >= fireInterval && safety < maxShotsPerFrame)
        {
            _timer -= fireInterval;
            Shoot();
            safety++;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
    }
}
