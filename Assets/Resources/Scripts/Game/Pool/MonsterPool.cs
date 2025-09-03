using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

public class MonsterPool : Singleton<MonsterPool>
{
    [SerializeField] private BaseMonster _normalMonsterPrefab;
    private ObjectPool<BaseMonster> _normalMonsterPool;

    private const int MAXSIZE = 1000;
    private const int INITSIZE = 200;

    protected override void OnAwake()
    {
        _normalMonsterPool = new ObjectPool<BaseMonster>(CreateNormalMonster, ActiavateNormalMonster, DisableNormalMonster, DestroyNormalMonster, false, INITSIZE, MAXSIZE);
    }

    private BaseMonster CreateNormalMonster()
    {
        return Instantiate(_normalMonsterPrefab);
    }

    private void ActiavateNormalMonster(BaseMonster monster)
    {
        monster.gameObject.SetActive(true);
    }
    private void DisableNormalMonster(BaseMonster monster)
    {
        monster.gameObject.SetActive(false);
    }

    private void DestroyNormalMonster(BaseMonster monster)
    {
        Destroy(monster);
    }

    public BaseMonster GetNormalMonster()
    {
        BaseMonster monster = null;
        if(_normalMonsterPool.CountActive >= MAXSIZE)
        {
            monster = CreateNormalMonster();
        }
        else
        {
            monster = _normalMonsterPool.Get();
        }

        return monster;
    }

    public void ReleaseNormalMonster(BaseMonster monster)
    {
        _normalMonsterPool.Release(monster);
    }
}
