using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _monsterTransform;

    private float _spawnTime = 0f;

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        _spawnTime += Time.deltaTime;
        
        if(_spawnTime > PlayerStat.CurSpawnTime)
        {
            _spawnTime = 0f;
            Spawn();
        }        
    }

    private void Spawn()
    {
        BaseMonster monster = MonsterPool.Instance.GetNormalMonster();
        monster.Init();
        monster.GetTransform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        monster.GetTransform.SetParent(_monsterTransform);
    }
}
