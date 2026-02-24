using UnityEngine;

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

    private void Start()
    {
        currentState = GameState.Playing;
    }
}