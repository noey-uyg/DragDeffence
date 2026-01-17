using UnityEngine;
using UnityEngine.UI;

public class Center : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _maxHP;
    private float _currentHP;
    private float _defense;
    private float _visualRadius;

    public float VisualRadius { get { return _visualRadius; } }

    public void Init()
    {
        _maxHP = PlayerStat.CurMaxHP;
        _currentHP = _maxHP;
        _defense = PlayerStat.CurDamageReduction;
        _visualRadius = _spriteRenderer.bounds.extents.x;

        if(_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHP;
            _hpSlider.value = _currentHP;
        }
    }

    public void TakeDamage(float monsterAtk)
    {
        float fianlDamage = Mathf.Max(1f, monsterAtk - _defense);
        _currentHP -= fianlDamage;

        if(_hpSlider != null)
        {
            _hpSlider.value = _currentHP;
        }

        if(_currentHP <= 0f)
        {
            GameManager.Instance.SetGameState(GameState.GameOver);
        }
    }
}
