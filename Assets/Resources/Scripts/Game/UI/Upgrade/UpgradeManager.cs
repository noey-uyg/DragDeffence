using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private UpgradeNode _nodePrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private UpgradeTreeConnector _upgradeTreeConnector;
    private float _gridScale = 150f;

    private List<UpgradeNode> _allNodes = new List<UpgradeNode>();

    public void InitializeAllNodes()
    {
        _allNodes.Clear();

        foreach (var v in CSVParser.UpgradeDataDict)
        {
            UpgradeData data = v.Value;

            var node = Instantiate(_nodePrefab, _contentParent);

            node.SetData(data);

            RectTransform rect = node.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(data.GridX * _gridScale, data.GridY * _gridScale);

            _upgradeTreeConnector.SetNodeRect(data.ID, rect);

            _allNodes.Add(node);
        }

        _upgradeTreeConnector.CreateAllConnection(_allNodes);
        LoadUpgradeData();
        NotifyNodeCleared();
    }

    public bool CheckIfCleared(int id, bool maxCheck)
    {
        foreach(var node in _allNodes)
        {
            if(node.UpgradeData.ID == id)
            {
                return maxCheck ? 
                    node.UpgradeData.level >= node.UpgradeData.MaxLevel :
                    node.UpgradeData.level > 0;
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

        _upgradeTreeConnector.RefreshAllLines();
    }

    public void ApplyUpgrade(UpgradeType type, int uniqueID, float value, int level)
    {
        if((int)type > 1000)
        {
            SkillStat.UpdateSkillContribution(type, uniqueID, value, level);
        }
        else
        {
            PlayerStat.UpdateContiribution(type, uniqueID, value);
        }
    }

    public void SaveUpgradeData()
    {
        UpgradeSaveData saveData = new UpgradeSaveData();

        foreach(var node in _allNodes)
        {
            saveData.datas.Add(new NodeSaveData
            {
                id = node.UpgradeData.ID,
                level = node.UpgradeData.level
            });
        }

        DataManager.SaveUpgradeData(saveData);
    }

    public void LoadUpgradeData()
    {
        UpgradeSaveData saveData = DataManager.LoadUpgradeData();

        if (saveData == null) return;

        foreach (var v in saveData.datas)
        {
            UpgradeNode target = _allNodes.Find(x => x.UpgradeData.ID == v.id);
            if(target != null)
            {
                target.UpgradeData.level = v.level;

                if (v.level > 0)
                {
                    float val = target.UpgradeData.Value[v.level];
                    ApplyUpgrade(target.UpgradeData.Type, target.UpgradeData.ID, val, v.level);
                }
            }
        }
    }
}
