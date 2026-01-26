using System.Collections.Generic;
using UnityEngine;

public class UpgradeTreeConnector : MonoBehaviour
{
    [SerializeField] private UpgradeLine _linePrefab;
    [SerializeField] private Transform _parent;

    private Dictionary<int, RectTransform> nodeRects = new Dictionary<int, RectTransform>();
    private List<UpgradeLine> _allLines = new List<UpgradeLine>();

    public void SetNodeRect(int id, RectTransform rectTransform)
    {
        if (nodeRects.ContainsKey(id)) return;

        nodeRects[id] = rectTransform;
    }
    
    public void CreateAllConnection(List<UpgradeNode> datas)
    {
        foreach(var v in datas)
        {
            if(v.UpgradeData.connectID != 0 && nodeRects.ContainsKey(v.UpgradeData.connectID))
            {
                UpgradeNode parent = datas.Find(x => x.UpgradeData.ID == v.UpgradeData.connectID);
                if(parent != null)
                {
                    DrawLine(parent, v);
                }
            }
        }
    }

    private void DrawLine(UpgradeNode parent, UpgradeNode child)
    {
        var line = Instantiate(_linePrefab, _parent);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        RectTransform start = nodeRects[parent.UpgradeData.ID];
        RectTransform end = nodeRects[child.UpgradeData.ID];

        Vector2 startPos = start.anchoredPosition;
        Vector2 endPos = end.anchoredPosition;

        Vector2 direction = endPos - startPos;
        float distance = direction.magnitude;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        lineRect.sizeDelta = new Vector2(distance, 5f);
        lineRect.anchorMin = start.anchorMin;
        lineRect.anchorMax = start.anchorMax;
        lineRect.pivot = new Vector2(0, 0.5f);
        lineRect.anchoredPosition = startPos;
        lineRect.localRotation = Quaternion.Euler(0, 0, angle);

        line.Init(parent, child.UpgradeData.connectMax);
        _allLines.Add(line);
    }

    public void RefreshAllLines()
    {
        foreach(var line in _allLines)
        {
            line.RefreshLine();
        }
    }
}
