using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private void Start()
    {
        UpgradeManager.Instance.InitializeAllNodes(this.gameObject);
    }

    public void OnClickStart()
    {
        gameObject.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void OnClickMain()
    {
        gameObject.SetActive(false);
        GameManager.Instance.SetGameState(GameState.Lobby);
    }
}
