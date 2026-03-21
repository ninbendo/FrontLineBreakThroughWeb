using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Playing,
        Clear,
        GameOver
    }

    [SerializeField] private GameState currentState = GameState.Ready;

    // ★追加：UI司令塔（Inspectorで紐づける）
    [SerializeField] private GameUIController gameUIController;

    public GameState CurrentState => currentState;
    public bool IsPlaying => currentState == GameState.Playing;
    public bool IsGameOver => currentState == GameState.GameOver;
    public bool IsClear => currentState == GameState.Clear;

    private void Start()
    {
        currentState = GameState.Playing;
        Debug.Log("[GameManager] State -> Playing");

        // ★任意：開始時は結果UIを消す（保険）
        if (gameUIController != null)
        {
            gameUIController.HideResult();
        }
    }

    public void SetGameOver()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        currentState = GameState.GameOver;
        Debug.Log("[GameManager] State -> GameOver");

        // ★追加：GameOver表示（ResultPanelを出す）
        if (gameUIController != null)
        {
            gameUIController.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("[GameManager] gameUIController is not assigned.");
        }
    }

    public void SetClear()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.Clear;
        Debug.Log("[GameManager] State -> Clear");

        if (gameUIController != null)
        {
            gameUIController.ShowClear();
        }
    }

    public void OnClickRetry()
    {
        Retry();
    }

    private void Retry()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log($"[GameManager] Retry -> {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
