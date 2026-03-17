using UnityEngine;

public sealed class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float lifetimeSec = 12f;

    private GameManager gameManager;
    private float _life;

    public bool IsDead { get; private set; }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public bool TryKill()
    {
        if (IsDead) return false;

        IsDead = true;

        var col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        return true;
    }

    private void Update()
    {
        if (IsDead) return;

        if (gameManager != null && !gameManager.IsPlaying)
        {
            return;
        }

        // Z-方向へ移動（奥→手前へ流れる）
        transform.position += -Vector3.forward * (speed * Time.deltaTime);

        _life += Time.deltaTime;
        if (_life >= lifetimeSec)
        {
            Destroy(gameObject);
        }
    }

}
