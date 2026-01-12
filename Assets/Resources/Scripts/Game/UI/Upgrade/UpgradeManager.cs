using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private UpgradeNode[] _allNodes;

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
}
