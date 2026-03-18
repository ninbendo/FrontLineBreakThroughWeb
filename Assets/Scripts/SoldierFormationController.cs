using UnityEngine;
using System.Collections.Generic;

public class SoldierFormationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject soldierPrefab;

    [Header("Formation")]
    [SerializeField] private float spacing = 1.2f;

    private readonly List<SoldierUnit> _soldiers = new List<SoldierUnit>();
    private PlayerGroupController _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<PlayerGroupController>();
    }

    /// <summary>
    /// 表示兵士数を目標値に合わせて生成/破棄する（v1: 簡易横並び配置）
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
    /// v1: 簡易配置（横一列）。Day 2でリング配置に進化予定。
    /// </summary>
    private void ArrangePositions()
    {
        int count = _soldiers.Count;
        if (count == 0) return;

        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            if (_soldiers[i] == null) continue;
            _soldiers[i].transform.localPosition = new Vector3(startX + i * spacing, 0f, 0f);
        }
    }
}
