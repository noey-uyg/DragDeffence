using UnityEngine;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;

    private void Start()
    {
        _upgradeButton.onClick.AddListener(UpgradeButtonClick);
    }

    public void UpgradeButtonClick()
    {
        PopupManager.Instance.ShowPopup<UpgradePopup>();
        GlobalManager.Instance.SetOnUI(true);
    }
}
