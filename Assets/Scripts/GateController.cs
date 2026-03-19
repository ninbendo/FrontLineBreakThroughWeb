using UnityEngine;

public sealed class GateController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2.5f;

    [SerializeField] private int effectValue = 1;

    private bool _used;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        if (_used) return;

        if (gameManager != null && !gameManager.IsPlaying)
        {
            return;
        }

        // Z-方向へ移動（奥→手前へ流れる）
        transform.position += -Vector3.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_used) return;
        if (!other.CompareTag("Player")) return;

        var pg = other.GetComponentInParent<PlayerGroupController>();
        if (pg == null) return;

        _used = true;

        if (effectValue > 0)
            pg.AddSoldiers(effectValue);
        else if (effectValue < 0)
            pg.RemoveSoldiers(-effectValue);

        Debug.Log($"[GateController] Applied effect: {effectValue}");
        Destroy(gameObject);
    }
}
