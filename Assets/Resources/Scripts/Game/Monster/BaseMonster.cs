using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] protected int _monsterID;
    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected Transform _transform;

    private float _realHP;
    private float _realAtk;
    private float _realSpeed;
    private float _rewardGold;
    private float _visualRadius;
    private Action<BaseMonster> _deadAction;

    public int MonsterID { get { return _monsterID; } }
    public Transform GetTransform { get { return _transform; } }
    public float VisualRadius { get { return _visualRadius; } }

    public void MoveToTarget(Vector2 targetPos, float deltaTime)
    {
        Vector2 currentPos = _transform.position;
        Vector2 dir = targetPos - currentPos;
        float distance = dir.magnitude;

        if(distance <= 0.5f)
        {
            AttackCenter();
            return;
        }

        Vector2 toCenterDir = dir / distance;

        Vector2 tangetDir = new Vector2(toCenterDir.y, -toCenterDir.x);

        float approachWeight = 0.5f;
        float orbitWeight = 1.0f;

        Vector2 moveDir = (toCenterDir * approachWeight) + (tangetDir * orbitWeight);
        moveDir = moveDir.normalized;

        Vector2 nextPos = currentPos + moveDir * _realSpeed * deltaTime;
        _transform.position = nextPos;
    }

    public void Init(MonsterData data)
    {
        _monsterID = data.monsterID;
        _spriteRenderer.sprite = data.sprite;

        _transform.localScale = new Vector3(data.baseScale, data.baseScale, 1);
        _visualRadius = _spriteRenderer.bounds.extents.x;

        _realHP = data.baseHP;
        _realAtk = data.baseAtk;
        _realSpeed = data.baseSpeed;
        _rewardGold = data.rewardGold;

        MonsterManager.Instance.Register(this);
        _deadAction = null;
    }

    public void TakeDamage(float dam)
    {
        _realHP -= dam;
        if (_realHP <= 0)
        {
            Die(true);
        }
    }

    private void AttackCenter()
    {
        GameManager.Instance.OnMonsterAttackCenter(_realAtk);
        Die(false);
    }

    public void Die(bool isKillByPlayer)
    {
        _deadAction?.Invoke(this);

        if (isKillByPlayer)
        {
            int finalGold = Mathf.RoundToInt(_rewardGold * PlayerStat.CurGoldGainPercent);
            PlayerStat.CurGold += finalGold;
        }

        MonsterPool.Instance.ReleaseNormalMonster(this);
        MonsterManager.Instance.Unregister(this);
    }

    public void SetDeadAction(Action<BaseMonster> action)
    {
        _deadAction = action;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // 런타임이 아닐 때도 대략적인 범위를 확인하기 위해 초기화 시점 외에도 계산
        if (!Application.isPlaying && _spriteRenderer != null && _spriteRenderer.sprite != null)
        {
            _visualRadius = _spriteRenderer.bounds.extents.x * 0.9f;
        }

        if (_visualRadius > 0)
        {
            // 판정 범위를 녹색 원으로 표시
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _visualRadius);

            // 중심점을 작은 점으로 표시
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.05f);
        }
    }
#endif
}
