using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NomalTrain : Train
{
    [SerializeField] private Animation _frontDoorAnimation;

    [SerializeField] private GameObject[] _objectsInTrainPrefab; // 기차 내 오브젝트 프리팹이 모두 있는 배열
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
        // 오브젝트들 모두 생성
        SpawnPointSystem system = Instantiate(_objectsInTrainPrefab[UnityEngine.Random.Range(0, _objectsInTrainPrefab.Length)], 
            transform.position, Quaternion.identity).GetComponent<SpawnPointSystem>();
        
        // 초기화
        _enemyCount = system.EnemyCount;
        system.Init(KillEnemy);
        system.transform.parent = transform;
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