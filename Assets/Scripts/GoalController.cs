using UnityEngine;

public sealed class GoalController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2.5f;

    private bool _reached;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (_reached) return;
        if (gameManager != null && !gameManager.IsPlaying) return;

        transform.position += -Vector3.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_reached) return;
        if (!other.CompareTag("Player")) return;

        _reached = true;

        if (gameManager != null)
        {
            gameManager.SetClear();
        }

        Debug.Log("[GoalController] Goal reached!");
    }
}
