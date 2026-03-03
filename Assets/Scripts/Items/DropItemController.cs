using UnityEngine;

public class DropItemController : MonoBehaviour
{
    [Header("Visual Only")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float lifeTime = 2f;

    private float _age;

    private void Update()
    {
        // Day7時点では演出用。
        // 将来は自動吸着 / 接触取得 / 自動回収へ拡張予定。
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        _age += Time.deltaTime;
        if (_age >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private bool _isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollected) return;

        Debug.Log($"[DropItemController] Trigger enter: {other.name}");

        if (!other.CompareTag("Player")) return;

        var player = other.GetComponentInParent<PlayerGroupController>();
        if (player == null)
        {
            Debug.LogWarning("[DropItemController] PlayerGroupController not found in parent.");
            return;
        }

        _isCollected = true;

        player.UpgradeWeaponLevel();

        Debug.Log("[DropItemController] Collected and applied weapon upgrade.");

        Destroy(gameObject);
    }
}
