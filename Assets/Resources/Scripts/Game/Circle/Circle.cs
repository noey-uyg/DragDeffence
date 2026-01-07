using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private int _baseAtk = 10;
    private float _baseDamageDelay = 0.1f;
    private float _radius = 0.5f;

    private float _timer = 0f;

    public float Radius { get { return _radius; } }
    public int AtkDamage { get { return _baseAtk; } }
    public Transform GetTransform {  get { return _transform; } }

    public bool IsReady()
    {
        _timer += Time.deltaTime;

        return _timer >= _baseDamageDelay;
    }

    public void ResetTimer()
    {
        _timer = 0f;
    }
}
