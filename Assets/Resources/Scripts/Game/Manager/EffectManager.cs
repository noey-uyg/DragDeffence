using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectManager
{
    private static readonly Dictionary<float, WaitForSeconds> _wfsCache = new Dictionary<float, WaitForSeconds>();
    
    public static void PlayEffect(EffectType type, Vector2 position)
    {
        PlayEffect(type, position, Quaternion.identity, Vector3.one);
    } 

    public static void PlayEffect(EffectType type, Vector2 position, Quaternion rotation)
    {
        PlayEffect(type, position, rotation, Vector3.one);
    }

    public static void PlayEffect(EffectType type, Vector2 position, Quaternion rotation, Vector3 scale)
    {
        var ps = EffectPool.Instance.GetEffect(type);
        if (ps == null) return;

        Transform t = ps.transform;
        t.position = position;
        t.rotation = rotation;
        t.localScale = scale;

        ps.Play();

        float duration = ps.main.duration;

        if (!_wfsCache.TryGetValue(duration, out var wfs))
        {
            wfs = new WaitForSeconds(duration);
            _wfsCache.Add(duration, wfs);
        }

        EffectPool.Instance.StartCoroutine(IEReleaseRoutine(type, ps, wfs));
    }


    private static IEnumerator IEReleaseRoutine(EffectType type, ParticleSystem ps, WaitForSeconds wfs)
    {
        yield return wfs;

        EffectPool.Instance.ReleaseEffect(type, ps);
    }
}
