using UnityEngine;

public class SoldierUnit : MonoBehaviour
{
    private PlayerGroupController _owner;
    private bool _isAlive = true;

    public bool IsAlive() => _isAlive;

    public void Initialize(PlayerGroupController owner)
    {
        _owner = owner;
        _isAlive = true;
    }

    /// <summary>
    /// 兵士を死亡させる。HP管理はDay 5で実装予定。
    /// </summary>
    public void Kill()
    {
        if (!_isAlive) return;
        _isAlive = false;
    }
}
