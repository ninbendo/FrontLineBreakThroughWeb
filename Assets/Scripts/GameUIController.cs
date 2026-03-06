using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TMP_Text soldierCountText;
    [SerializeField] private TMP_Text weaponLevelText;
    [SerializeField] private Button soundToggleButton;
    [SerializeField] private TMP_Text soundToggleButtonLabel; // ボタン内Text(TMP)

    [Header("Result UI (display only)")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text remainCountText;

    [Header("Sources")]
    [SerializeField] private PlayerGroupController playerGroup;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        if (resultPanel != null) resultPanel.SetActive(false);

        if (soundToggleButton != null)
        {
            soundToggleButton.onClick.RemoveAllListeners();
            soundToggleButton.onClick.AddListener(OnSoundToggleClicked);
        }

        RefreshAll();
    }

    private void Update()
    {
        RefreshHud();
    }

    public void ShowGameOver()
    {
        if (resultPanel != null) resultPanel.SetActive(true);
        if (resultText != null) resultText.text = "GAME OVER";
        RefreshRemain();
    }

    public void HideResult()
    {
        if (resultPanel != null) resultPanel.SetActive(false);
    }

    private void RefreshAll()
    {
        RefreshHud();
        RefreshSoundLabel();
        RefreshRemain();
    }

    private void RefreshHud()
    {
        if (playerGroup == null) return;

        // Day9暫定：人数=HP表示
        if (soldierCountText != null)
            soldierCountText.text = $"HP: {playerGroup.CurrentHp}";

        // Day9暫定：武器Lvは後で接続
        if (weaponLevelText != null)
            weaponLevelText.text = $"Weapon Lv: {playerGroup.WeaponLevel}";
    }

    private void RefreshRemain()
    {
        if (remainCountText == null) return;
        remainCountText.text = "Remain: ?";
    }

    private void RefreshSoundLabel()
    {
        if (soundToggleButtonLabel == null || audioManager == null) return;
        soundToggleButtonLabel.text = audioManager.IsMuted ? "SOUND: OFF" : "SOUND: ON";
    }

    private void OnSoundToggleClicked()
    {
        if (audioManager == null) return;
        audioManager.ToggleMute();
        RefreshSoundLabel();
    }
}
