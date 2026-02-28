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
        // Y+方向へ移動
        transform.position += Vector3.up * speed * Time.deltaTime;

        // 寿命で破棄（無限増殖防止）
        _age += Time.deltaTime;
        if (_age >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ✅ 2回目以降は無視（連動Destroy防止）
        if (_hasHit) return;

        if (!other.CompareTag("Enemy")) return;

        _hasHit = true;

        // ✅ 追加のOnTriggerを止める（同フレーム多重呼び出し対策）
        if (_col != null) _col.enabled = false;

        // ✅ 「敵のルート（EnemyController持ち）」を確実に消す
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
            // 予備：親にいない場合だけ other 自体を消す
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
}
