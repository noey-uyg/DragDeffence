using UnityEngine;

public class UpgradeLine : MonoBehaviour
{
    private UpgradeNode _parentNode;
    private bool _requiresMax;

    public void Init(UpgradeNode parent, bool requiresMax)
    {
        _parentNode = parent;
        _requiresMax = requiresMax;
        RefreshLine();
    }

    public void RefreshLine()
    {
        if (_parentNode == null) return;

        var data = _parentNode.UpgradeData;
        bool isConditionMet = false;

        if (_requiresMax){
            isConditionMet = data.level >= data.MaxLevel;
        }
        else
        {
            isConditionMet = data.level > 0;
        }

        gameObject.SetActive(isConditionMet);
    }
}
