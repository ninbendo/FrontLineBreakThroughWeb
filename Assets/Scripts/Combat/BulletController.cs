using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 3f;

    private float _age;

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
        // （必要ならログは外す）
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
