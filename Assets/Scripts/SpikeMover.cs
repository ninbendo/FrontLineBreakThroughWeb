using UnityEngine;

public sealed class SpikeMover : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;

    private void Update()
    {
        transform.position += Vector3.down * (speed * Time.deltaTime);
    }
}