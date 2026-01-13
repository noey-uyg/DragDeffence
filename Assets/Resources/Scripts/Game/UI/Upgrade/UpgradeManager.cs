using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private List<UpgradeNode> _allNodes;

    public void InitializeAllNodes(GameObject panel)
    {
        UpgradeNode[] nodes = panel.GetComponentsInChildren<UpgradeNode>();

        foreach (UpgradeNode node in nodes)
        {
            if(!_allNodes.Contains(node))
                _allNodes.Add(node);
        }

        NotifyNodeCleared();
    }

    public bool CheckIfCleared(int id)
    {
        foreach(var node in _allNodes)
        {
            if(node.UpgradeData.ID == id)
            {
                return node.UpgradeData.level > 0;
            }
        }

        return false;
    }

    public void NotifyNodeCleared()
    {
        foreach(var node in _allNodes)
        {
            node.RefreshNodeStatus();
        }
    }

    public void ApplyUpgrade(UpgradeType type, int uniqueID, float value)
    {
        PlayerStat.UpdateContiribution(type, uniqueID, value);
    }
}
