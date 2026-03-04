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

    public GameState CurrentState => currentState;
    public bool IsPlaying => currentState == GameState.Playing;
    public bool IsGameOver => currentState == GameState.GameOver;
    public bool IsClear => currentState == GameState.Clear;

    private void Start()
    {
        currentState = GameState.Playing;
        Debug.Log("[GameManager] State -> Playing");
    }

    public void SetGameOver()
    {
        if (currentState != GameState.Playing)
        {
            return;
        }

        currentState = GameState.GameOver;
        Debug.Log("[GameManager] State -> GameOver");
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
