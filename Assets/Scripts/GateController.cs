using UnityEngine;

public sealed class GateController : MonoBehaviour
{
    [SerializeField] private int effectValue = 1;

    private bool _used;

    private void OnTriggerEnter(Collider other)
    {
        if (_used) return;
        if (!other.CompareTag("Player")) return;

        var pg = other.GetComponentInParent<PlayerGroupController>();
        if (pg == null) return;

        _used = true;

        if (effectValue > 0)
            pg.Heal(effectValue);
        else if (effectValue < 0)
            pg.TakeDamage(-effectValue);

        Debug.Log($"[GateController] Applied effect: {effectValue}");
        Destroy(gameObject);
    }
}
