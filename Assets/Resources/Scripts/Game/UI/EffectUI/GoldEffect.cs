using DG.Tweening;
using TMPro;
using UnityEngine;

public class GoldEffect : EffectUI
{
    public void Show(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        Init(startWorldPos);
        _transform.localScale = Vector3.zero;

        _sequence = DOTween.Sequence()
            .SetAutoKill(true)
            .OnComplete(OnReleaseToPool);

        _sequence.Append(_transform.DOJump(endWorldPos, 0.2f, 1, 1f).SetEase(Ease.InQuad));
        _sequence.Join(_transform.DOScale(0.2f, 1f));
    }

    protected override void OnReleaseToPool()
    {
        MainHUD.Instance.PunchGoldIcon();
        EffectUIPool.Instance.Release(this);
    }
}
