using System;
using System.Collections.Generic;
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

    /// <summary>
    /// 초기화 함수
    /// </summary>
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
        if (_isClear == true)
            return;

        _isClear = true;
        _frontDoorAnimation.Play(); // 문 Open

        Instantiate(treasureBox, treasureBoxSpawnPoint.position, Quaternion.identity); // 상자 생성 
    }

    /// <summary>
    /// 적이 죽었을 때 실행되는 함수
    /// </summary>
    private void KillEnemy()
    {
        _enemyCount -= 1;

        // 적 카운트가 0이 되면 게임 클리어
        if (_enemyCount <= 0)
        {
            ClearStage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameManager.Instance.NextStage();
        }
    }
}

[Serializable]
class EnemySpawnPoint
{
    public Transform enemySpawmPointTr;
    public GameObject enemy;
}