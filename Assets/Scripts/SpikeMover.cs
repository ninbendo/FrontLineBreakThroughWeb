using UnityEngine;

public sealed class SpikeMover : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (gameManager != null && !gameManager.IsPlaying)
        {
            return;
        }

        transform.position += Vector3.down * (speed * Time.deltaTime);
    }
}
