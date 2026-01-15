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
        float sqrRadius = _circle.Radius * _circle.Radius;
        Vector2 circlePos = _circle.GetTransform.position;

        for (int i = _monsters.Count - 1; i >= 0; i--)
        {
            // 이동
            _monsters[i].MoveToTarget(_targetPosition, dt);

            // 데미지 처리
            if (_circle.IsReady())
            {
                float sqrDist = (circlePos - (Vector2)_monsters[i].GetTransform.position).sqrMagnitude;

                if (sqrDist <= sqrRadius)
                {
                    _monsters[i].TakeDamage(_circle.AtkDamage);
                }
            }
        }

        if(_circle.IsReady()) _circle.ResetTimer();
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
