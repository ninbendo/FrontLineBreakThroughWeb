using TMPro;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [Header("Gate Value")]
    [SerializeField] private int initialValue = -100;
    [SerializeField] private TMP_Text valueText;

    private int currentValue;
    private bool isApplied;

    private void Awake()
    {
        currentValue = initialValue;
        RefreshView();
    }

    public void ApplyBulletHit()
    {
        if (isApplied)
        {
            return;
        }

        currentValue = GateValueCalculator.ApplyBulletHit(currentValue);
        RefreshView();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isApplied)
        {
            return;
        }

        var playerGroup = other.GetComponentInParent<PlayerGroupController>();
        if (playerGroup == null)
        {
            return;
        }

        isApplied = true;
        playerGroup.ApplyGateValue(currentValue);
    }

    private void RefreshView()
    {
        if (valueText == null)
        {
            return;
        }

        valueText.text = currentValue >= 0
            ? $"+{currentValue}"
            : currentValue.ToString();
    }
}
