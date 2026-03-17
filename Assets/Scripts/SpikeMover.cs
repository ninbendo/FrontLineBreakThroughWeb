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

        // Z-方向へ移動（奥→手前へ流れる）
        transform.position += -Vector3.forward * (speed * Time.deltaTime);
    }
}
