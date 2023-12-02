using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrain : Train
{
    [SerializeField] private GameObject _tutorialObjectInTrainPrefab; // 기차 내 오브젝트 프리팹이 모두 있는 배열

    private int _enemyCount;

    public override void Start()
    {
        base.Start();
        Init();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public override void Init()
    {
        // 오브젝트들 모두 생성
        SpawnPointSystem system = Instantiate(_tutorialObjectInTrainPrefab,
            transform.position, Quaternion.identity).GetComponent<SpawnPointSystem>();

        // 초기화
        system.Init(KillEnemy);
        system.transform.parent = transform;
        _enemyCount = system.EnemyCount;
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
            Init();
        }
    }
}
