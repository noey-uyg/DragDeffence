using UnityEngine;
using static UnityEngine.CullingGroup;
using UnityEngine.SceneManagement;

public class GameManager : DontDestroySingleton<GameManager>
{
    public enum GameState { None, Playing, Paused, GameOver }

    private GameState _state;

    public GameState CurrentState { get { return _state; } }

    private void Start()
    {
        StartGame();
    }

    public void SetGameState(GameState state)
    {
        if (_state == state) return;

        _state = state;
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
    }
}
