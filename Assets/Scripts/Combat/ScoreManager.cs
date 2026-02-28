using System;

public static class ScoreManager
{
    public static int Score { get; private set; }

    public static event Action<int> OnScoreChanged;

    public static void Reset()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    public static void Add(int value)
    {
        if (value <= 0) return;

        Score += value;
        OnScoreChanged?.Invoke(Score);
    }
}
