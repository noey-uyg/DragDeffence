using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private Transform[] _spawnPoints;

    private float _baseSpawnTime = 1f;
    private float _spawnTime = 0f;

    private void Update()
    {
        _spawnTime += Time.deltaTime;
        
        if(_spawnTime > _baseSpawnTime)
        {
            _spawnTime = 0f;
            Spawn();
        }        
    }

    private void Spawn()
    {
        BaseMonster monster = MonsterPool.Instance.GetNormalMonster();
        monster.Init();
        monster.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }
}
