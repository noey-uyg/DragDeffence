using TMPro;
using UnityEngine;

public class UpgradeTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private RectTransform _rectTransform;

    public void Show(string title, string desc, string cost, int curLevel, int maxLevel, Vector2 position)
    {
        gameObject.SetActive(true);
        _titleText.text = title;
        _descText.text = desc;
        _costText.text = $"Cost : {cost}";
        _levelText.text = $"Lv. {curLevel} / {maxLevel}";

        _rectTransform.anchoredPosition = position + new Vector2(0, 60);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
