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
        if (_timer >= fireInterval)
        {
            _timer -= fireInterval;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
    }
}