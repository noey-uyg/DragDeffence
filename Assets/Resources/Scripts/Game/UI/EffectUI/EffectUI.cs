using DG.Tweening;
using UnityEngine;

public abstract class EffectUI : MonoBehaviour
{
    [SerializeField] protected Transform _transform;

    protected Sequence _sequence;

    protected virtual void Init(Vector3 worldPos)
    {
        KillSequence();
        _transform.position = worldPos;
    }

    protected abstract void OnReleaseToPool();

    protected void KillSequence()
    {
        if (_sequence != null && _sequence.IsActive()) _sequence.Kill();

        _sequence = null;
    }

    private void OnDisable()
    {
        KillSequence();
    }
}
