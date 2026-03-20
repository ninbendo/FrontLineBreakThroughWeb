using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 3f;

    private float _age;

    // ✅ 同フレームで複数ヒットを防ぐ
    private bool _hasHit;

    // ✅ 自分のColliderをキャッシュ（無効化に使う）
    private Collider _col;

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void Update()
    {
        // Z+方向へ移動（前方＝画面奥へ飛ぶ）
        transform.position += Vector3.forward * speed * Time.deltaTime;

        // 寿命で破棄（無限増殖防止）
        _age += Time.deltaTime;
        if (_age >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            ConsumeHit();
            var enemy = other.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                if (enemy.TryKill())
                {
                    ScoreManager.Add(1);
                    Destroy(enemy.gameObject);
                }
            }
            else
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Gate"))
        {
            ConsumeHit();
            var gate = other.GetComponentInParent<GateController>();
            if (gate != null) gate.OnBulletHit();
            Destroy(gameObject);
        }
        // Barrel側がBulletタグを検出して処理するため、ここでは不要
    }

    private void ConsumeHit()
    {
        _hasHit = true;
        if (_col != null) _col.enabled = false;
    }
}
