using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeData
{
    [Header("Info")]
    public UpgradeType Type;
    public int ID;
    public string Name;
    [TextArea(3,5)] public string Description;

    [Header("Status")]
    public int level;
    public int MaxLevel;
    public int[] cost;
    public float[] Value;

    [Header("Connection")]
    public int connectID;
}

public class UpgradeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UpgradeData _upgradeData;
    [SerializeField] private Button _nodeButton;
    [SerializeField] private Image _nodeImage;
    [SerializeField] private Transform _transform;

    public UpgradeData UpgradeData { get { return _upgradeData; } }

    public void RefreshNodeStatus()
    {
        if (_upgradeData.connectID == 0)
        {
            SetActivate(true);
            return;
        }

        bool isParentClear = UpgradeManager.Instance.CheckIfCleared(_upgradeData.connectID);
        SetActivate(isParentClear);
    }

    private void SetActivate(bool active)
    {
        gameObject.SetActive(active);
    }

    public void OnUpgradeClick()
    {
        if (_upgradeData.level >= _upgradeData.MaxLevel) return;

        float bonusValue = _upgradeData.Value[_upgradeData.level];
        _upgradeData.level++;
        UpgradeManager.Instance.ApplyUpgrade(_upgradeData.Type, _upgradeData.ID, bonusValue);
        UpgradeManager.Instance.NotifyNodeCleared();
        OnPointerEnter(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bool isMax = _upgradeData.level >= _upgradeData.MaxLevel;

        int cost = isMax ? 0 : _upgradeData.cost[_upgradeData.level];
        string costStr = isMax ? "<color=red>(Max)</color>" : cost.ToString();

        string finalDesc = _upgradeData.Description;
        float curTotalValue = _upgradeData.Value[_upgradeData.level];

        if (!isMax)
        {
            float nextTotalValue = _upgradeData.Value[_upgradeData.level + 1];
            float additionalValue = nextTotalValue - curTotalValue;

            finalDesc = string.Format(_upgradeData.Description, curTotalValue, additionalValue);
        }
        else
        {
            finalDesc = string.Format(_upgradeData.Description, curTotalValue, 0);
        }
        TooltipManager.Instance.ShowUpgradeTooltip(
            _upgradeData.Name,
            finalDesc,
            costStr,
            _upgradeData.level,
            _upgradeData.MaxLevel,
            _transform.position
            );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
