using UnityEngine;

public sealed class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float lifetimeSec = 12f;

    private float _life;

    public bool IsDead { get; private set; }

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

        transform.position += Vector3.down * (speed * Time.deltaTime);

        _life += Time.deltaTime;
        if (_life >= lifetimeSec)
        {
            Destroy(gameObject);
        }
    }

}
