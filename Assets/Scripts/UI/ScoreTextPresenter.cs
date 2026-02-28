using TMPro;
using UnityEngine;

public sealed class ScoreTextPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += HandleScoreChanged;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= HandleScoreChanged;
    }

    private void Start()
    {
        ScoreManager.Reset();
        HandleScoreChanged(ScoreManager.Score);
    }

    private void HandleScoreChanged(int score)
    {
        if (scoreText == null) return;
        scoreText.text = $"SCORE: {score}";
    }
}
