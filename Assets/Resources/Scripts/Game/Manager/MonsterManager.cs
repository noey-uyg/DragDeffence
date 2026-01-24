using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    private List<BaseMonster> _monsters = new List<BaseMonster>();
    [SerializeField] private Circle _circle;

    private Vector2 _targetPosition = Vector2.zero;

    public void Register(BaseMonster monster) => _monsters.Add(monster);
    public void Unregister(BaseMonster monster) => _monsters.Remove(monster);

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing || _monsters.Count == 0)
            return;

        float dt = Time.deltaTime;
        Vector2 circlePos = _circle.GetTransform.position;
        bool canAttack = _circle.IsReady();
        float circleRadius = _circle.Radius;

        if(canAttack) _circle.PlayAttackMotion();

        for (int i = _monsters.Count - 1; i >= 0; i--)
        {
            if(i>=_monsters.Count || _monsters[i] == null) continue;

            // 이동
            _monsters[i].MoveToTarget(_targetPosition, dt);

            // 데미지 처리
            if (canAttack)
            {
                float sqrDist = (circlePos - (Vector2)_monsters[i].GetTransform.position).sqrMagnitude;
                float checkRadius = circleRadius + _monsters[i].VisualRadius;
                float sqrCheckRadius = checkRadius * checkRadius;

                if (sqrDist <= sqrCheckRadius)
                {
                    var (finalDam, isCritical) = _circle.GetCalcDamage();
                    _monsters[i].TakeDamage(finalDam, isCritical);
                }
            }
        }

        if(canAttack) _circle.ResetTimer();
    }

    public void ClearAllMonsters()
    {
        for(int i = _monsters.Count - 1; i >= 0; i--)
        {
            if (_monsters[i] != null)
            {
                _monsters[i].Die(false);
            }
        }

        _monsters.Clear();
    }
}
