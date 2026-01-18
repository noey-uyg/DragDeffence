using UnityEngine;

public class TooltipManager : Singleton<TooltipManager>
{
    [SerializeField] private UpgradeTooltip _upgradeTooltip;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private Camera _UICamera;

    public void ShowUpgradeTooltip(string title, string desc, string cost, int curLevel, int maxLevel, Vector2 position)
    {
        _upgradeTooltip.Show(title, desc, cost, curLevel, maxLevel, position);
    }

    public void HideTooltip()
    {
        _upgradeTooltip.Hide();
    }
}
