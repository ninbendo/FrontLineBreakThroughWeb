using UnityEngine;
using TMPro;

public sealed class GateController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2.5f;

    [Header("Gate Value")]
    [SerializeField] private int initialValue = -100;

    [Header("Display")]
    [SerializeField] private TMP_Text valueText;

    [Header("Color")]
    [SerializeField] private Renderer gateRenderer;
    [SerializeField] private Material positiveMaterial;
    [SerializeField] private Material negativeMaterial;

    private int currentValue;
    private bool _applied;
    private GameManager gameManager;

    public int CurrentValue => currentValue;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        currentValue = initialValue;
    }

    private void Start()
    {
        UpdateDisplay();
    }

    private void Update()
    {
        if (gameManager != null && !gameManager.IsPlaying) return;

        // Z-方向へ移動（奥→手前へ流れる）
        transform.position += -Vector3.forward * (speed * Time.deltaTime);
    }

    /// <summary>
    /// 弾命中時に呼ばれる。1発で値が+1変化する。
    /// </summary>
    public void OnBulletHit(int amount = 1)
    {
        if (_applied) return;
        currentValue += amount;
        UpdateDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_applied) return;
        if (!other.CompareTag("Player")) return;

        var pg = other.GetComponentInParent<PlayerGroupController>();
        if (pg == null) return;

        _applied = true;

        if (currentValue > 0)
            pg.AddSoldiers(currentValue);
        else if (currentValue < 0)
            pg.RemoveSoldiers(-currentValue);

        Debug.Log($"[GateController] Applied value: {currentValue}");
        Destroy(gameObject);
    }

    private void UpdateDisplay()
    {
        if (valueText != null)
        {
            string prefix = currentValue >= 0 ? "+" : "";
            valueText.text = $"{prefix}{currentValue}";
        }

        if (gateRenderer != null)
        {
            if (currentValue >= 0 && positiveMaterial != null)
                gateRenderer.material = positiveMaterial;
            else if (currentValue < 0 && negativeMaterial != null)
                gateRenderer.material = negativeMaterial;
        }
    }
}
