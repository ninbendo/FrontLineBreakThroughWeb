using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    [SerializeField] private int maxHp = 2;

    private int _currentHp;
    private PlayerGroupController _owner;
    private bool _isAlive = true;
    private Renderer _renderer;
    private Color _originalColor;

    public bool IsAlive() => _isAlive;
    public int CurrentHp => _currentHp;

    public void Initialize(PlayerGroupController owner)
    {
        _owner = owner;
        _currentHp = maxHp;
        _isAlive = true;

        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    /// <summary>
    /// ダメージを受ける。HP=0で死亡。
    /// </summary>
    public void TakeDamage(int amount = 1)
    {
        if (!_isAlive) return;

        _currentHp = Mathf.Max(0, _currentHp - amount);
        Debug.Log($"[SoldierUnit] TakeDamage: HP={_currentHp}");

        if (_currentHp <= 0)
        {
            Kill();
        }
        else
        {
            ShowDamagedVisual();
        }
    }

    private void ShowDamagedVisual()
    {
        if (_renderer != null)
        {
            _renderer.material.color = Color.red;
        }
    }

    private void Kill()
    {
        if (!_isAlive) return;
        _isAlive = false;
        Debug.Log("[SoldierUnit] Killed.");
    }
}
