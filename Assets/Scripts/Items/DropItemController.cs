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
        // Z-方向へ移動（奥→手前へ流れる）
        transform.position += -Vector3.forward * moveSpeed * Time.deltaTime;

        _age += Time.deltaTime;
        if (_age >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
