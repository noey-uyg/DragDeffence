using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : PopupBase
{
    [SerializeField] private TextMeshProUGUI _earnedGoldText;
    [SerializeField] private TextMeshProUGUI _totalGoldText;
    [SerializeField] private TextMeshProUGUI _survivalTimeText;

    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _mainButton;

    public void Init(int earnedGold, float survivalTime)
    {
        _survivalTimeText.text = $"{survivalTime:F2}s";
        _earnedGoldText.text = $"+ {earnedGold:N0}";
        _totalGoldText.text = $"{PlayerStat.CurGold:N0}";

        _retryButton.onClick.RemoveAllListeners();
        _retryButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.HideTopPopup();
            GameManager.Instance.StartGame();
        });

        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.HideTopPopup();
            GameManager.Instance.SetGameState(GameState.Upgrade);
            GameManager.Instance.OnUpgradePanel();
        });

        _mainButton.onClick.RemoveAllListeners();
        _mainButton.onClick.AddListener(() =>
        {
            PopupManager.Instance.HideTopPopup();
            GameManager.Instance.SetGameState(GameState.Lobby);
        });
    }
}
