using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] protected int _monsterID;
    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    protected float _baseHP = 100;
    protected float _baseAtk = 1;
    protected float _baseSpeed = 1;

    private float _realHP;
    private float _realAtk;
    private float _realSpeed;
    private Vector2 _centerPosition = Vector2.zero;
    private Action<BaseMonster> _deadAction;

    public int MonsterID { get { return _monsterID; } }

    private void FixedUpdate()
    {
        Vector2 dirVec = _centerPosition - _rigidBody.position;
        Vector2 nextVec = dirVec.normalized * _realSpeed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(_rigidBody.position + nextVec);
    }

    public void Init()
    {
        _deadAction = null;
        _realHP = _baseHP;
        _realAtk = _baseAtk;
        _realSpeed = _baseSpeed;
    }

    public void TakeDamage(float dam)
    {
        _realHP -= dam;
        if (_realHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _deadAction?.Invoke(this);
        MonsterPool.Instance.ReleaseNormalMonster(this);
    }

    public void SetDeadAction(Action<BaseMonster> action)
    {
        _deadAction = action;
    }
}
