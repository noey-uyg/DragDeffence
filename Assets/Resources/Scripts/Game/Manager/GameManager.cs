using UnityEngine;
using static UnityEngine.CullingGroup;
using UnityEngine.SceneManagement;

public class GameManager : DontDestroySingleton<GameManager>
{
    private GameState _state;

    [SerializeField] private GameObject _center;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _titlePanel;

    public GameState CurrentState { get { return _state; } }

    public void SetGameState(GameState state)
    {
        if (_state == state) return;

        _state = state;
        ChagngeGameState();
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
    }

    private void ChagngeGameState()
    {
        switch (_state)
        {
            case GameState.Playing:
                _center.SetActive(true);
                _circle.SetActive(true);
                _titlePanel.SetActive(false);
                break;
            default:
                _center.SetActive(false);
                _circle.SetActive(false);
                _titlePanel.SetActive(true);
                break;

        }
    }
}
