using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSystem : MonoBehaviour
{
    public int EnemyCount { get; set; }

    [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;           // 몬스터 스폰 포인트

    /// <summary>
    /// 초기화함수
    /// </summary>
    /// <param name="enemyDieAction">적이 죽었을 때 실행할 함수</param>
    public void Init(Action enemyDieAction)
    {
        EnemyCount = _enemySpawnPoints.Length;

        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            Instantiate(_enemySpawnPoints[i].enemy, _enemySpawnPoints[i].enemySpawmPointTr.position,
                Quaternion.identity).GetComponentInChildren<EnemyController>()._dieEvent += enemyDieAction;
        }
    }
}
