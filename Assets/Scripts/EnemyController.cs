using UnityEngine;

public sealed class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float lifetimeSec = 12f;

    private float _life;

    private void Update()
    {
        transform.position += Vector3.down * (speed * Time.deltaTime);

        _life += Time.deltaTime;
        if (_life >= lifetimeSec)
        {
            Destroy(gameObject);
        }
    }

}
