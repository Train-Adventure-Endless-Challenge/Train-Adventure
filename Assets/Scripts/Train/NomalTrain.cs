using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NomalTrain : Train
{
    [SerializeField] private Animation _frontDoorAnimation;

    [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;           // 몬스터 스폰 포인트
        
    private List<GameObject> _currentTrainEnemys = new List<GameObject>();  // 현재 스테이지 몬스터

    private bool _isClear;                                                     

    private void Start()
    {
        // 동적타임에 NavMesh 생성하기
        NavMeshSurface surfaces = _floor.GetComponent<NavMeshSurface>();

        surfaces.RemoveData();
        surfaces.BuildNavMesh();

        Init(); // 임시 코드 
    }

    public override void Init()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            _currentTrainEnemys.Add(Instantiate(_enemySpawnPoints[i].enemy, _enemySpawnPoints[i].enemySpawmPointTr.position, Quaternion.identity));
        }
    }

    private void ClearStage()
    {
        _isClear = true;
    }
}


[Serializable]
class EnemySpawnPoint
{
    public Transform enemySpawmPointTr;
    public GameObject enemy;
}