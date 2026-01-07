using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    [SerializeField] private GameObject _mainUIPanel;

    public void OnStartButtonClick()
    {
        GameManager.Instance.StartGame();
    }

    private void OnDisable()
    {
        _mainUIPanel.SetActive(true);
    }
}
