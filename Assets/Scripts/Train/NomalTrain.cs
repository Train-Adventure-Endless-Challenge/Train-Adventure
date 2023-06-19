using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NomalTrain : Train
{
    [SerializeField] private Animation _frontDoorAnimation;

    [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;           // 몬스터 스폰 포인트

    [SerializeField] private GameObject treasureBox;
    [SerializeField] private Transform treasureBoxSpawnPoint;

    private int _enemyCount;

    private bool _isClear;                                                     

    private void Start()
    {
        // 동적타임에 NavMesh 생성하기
        NavMeshSurface surfaces = _floor.GetComponent<NavMeshSurface>();

        surfaces.RemoveData();
        surfaces.BuildNavMesh();
    }

    public override void Init()
    {
        _enemyCount = _enemySpawnPoints.Length;

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            Instantiate(_enemySpawnPoints[i].enemy, _enemySpawnPoints[i].enemySpawmPointTr.position, 
                Quaternion.identity).GetComponentInChildren<EnemyController>()._dieEvent += KillEnemy;
        }
    }

    private void ClearStage()
    {
        _isClear = true;
        _frontDoorAnimation.Play();
        Instantiate(treasureBox, treasureBoxSpawnPoint.position, Quaternion.identity);
    }

    private void KillEnemy()
    {
        _enemyCount -= 1;

        if (_enemyCount <= 0)
        {
            ClearStage();
        }
    }
}

[Serializable]
class EnemySpawnPoint
{
    public Transform enemySpawmPointTr;
    public GameObject enemy;
}