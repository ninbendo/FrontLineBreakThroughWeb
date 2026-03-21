using UnityEngine;
using System.Collections.Generic;

public class SoldierFormationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject soldierPrefab;

    [Header("Ring Radii")]
    [SerializeField] private float ring1Radius = 1.5f;
    [SerializeField] private float ring2Radius = 3.0f;
    [SerializeField] private float ring3Radius = 5.0f;
    [SerializeField] private float ring4Radius = 8.0f;

    // リングごとの定員: 6, 12, 20, 50（合計88 + 中心1 = 89）
    private static readonly int[] RingCapacities = { 6, 12, 20, 50 };

    private readonly List<SoldierUnit> _soldiers = new List<SoldierUnit>();
    private PlayerGroupController _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<PlayerGroupController>();
    }

    /// <summary>
    /// 表示兵士数を目標値に合わせて生成/破棄する
    /// </summary>
    public void RebuildFormation(int targetCount)
    {
        // 余分な兵士を削除
        while (_soldiers.Count > targetCount)
        {
            int last = _soldiers.Count - 1;
            var soldier = _soldiers[last];
            _soldiers.RemoveAt(last);
            if (soldier != null)
            {
                Destroy(soldier.gameObject);
            }
        }

        // 不足分を生成
        while (_soldiers.Count < targetCount)
        {
            if (soldierPrefab == null) break;

            var go = Instantiate(soldierPrefab, transform);
            var unit = go.GetComponent<SoldierUnit>();
            if (unit == null)
            {
                unit = go.AddComponent<SoldierUnit>();
            }
            unit.Initialize(_owner);
            _soldiers.Add(unit);
        }

        // 配置を更新
        ArrangePositions();
    }

    /// <summary>
    /// ダメージ対象の兵士を探す（外周優先、被弾済み→未被弾の順）
    /// </summary>
    public SoldierUnit FindDamageTarget()
    {
        // 外周から探索（リストの末尾が外周）
        // まず被弾済みを優先（2回目で倒しきる）
        for (int i = _soldiers.Count - 1; i >= 0; i--)
        {
            if (_soldiers[i] != null && _soldiers[i].IsDamaged)
                return _soldiers[i];
        }
        // 次に未被弾
        for (int i = _soldiers.Count - 1; i >= 0; i--)
        {
            if (_soldiers[i] != null && _soldiers[i].IsAlive())
                return _soldiers[i];
        }
        return null;
    }

    /// <summary>
    /// 死亡した兵士をリストから除去してGameObjectを破棄する
    /// </summary>
    public void RemoveDeadSoldier(SoldierUnit soldier)
    {
        _soldiers.Remove(soldier);
        if (soldier != null)
        {
            Destroy(soldier.gameObject);
        }
        ArrangePositions();
    }

    /// <summary>
    /// v2: リング配置（中心+4リング: 6/12/20/50、時計回り）
    /// </summary>
    private void ArrangePositions()
    {
        int count = _soldiers.Count;
        if (count == 0) return;

        float[] ringRadii = { ring1Radius, ring2Radius, ring3Radius, ring4Radius };

        for (int i = 0; i < count; i++)
        {
            if (_soldiers[i] == null) continue;
            _soldiers[i].transform.localPosition = GetPositionForIndex(i, ringRadii);
        }
    }

    /// <summary>
    /// index から配置座標を算出する
    /// index 0 = 中心、1-6 = リング1、7-18 = リング2、19-38 = リング3、39-88 = リング4
    /// </summary>
    private Vector3 GetPositionForIndex(int index, float[] radii)
    {
        // 中心（index 0）
        if (index == 0)
        {
            return Vector3.zero;
        }

        // どのリングに属するか判定
        int remaining = index - 1; // 中心を除いたindex
        for (int ring = 0; ring < RingCapacities.Length; ring++)
        {
            if (remaining < RingCapacities[ring])
            {
                float radius = radii[ring];
                int capacity = RingCapacities[ring];
                return CalculateRingPosition(remaining, capacity, radius);
            }
            remaining -= RingCapacities[ring];
        }

        // 89人を超えた場合（renderCapで制限されるため通常到達しない）
        return Vector3.zero;
    }

    /// <summary>
    /// リング上の位置を計算（時計回り、12時方向=Z+から開始）
    /// </summary>
    private Vector3 CalculateRingPosition(int indexInRing, int ringCapacity, float radius)
    {
        // 12時方向(Z+) = 90°から時計回り(角度減少)
        float angleStep = 360f / ringCapacity;
        float angleDeg = 90f - indexInRing * angleStep;
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = radius * Mathf.Cos(angleRad);
        float z = radius * Mathf.Sin(angleRad);

        return new Vector3(x, 0f, z);
    }
}
