using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NomalTrain : Train
{
    [SerializeField] private EnemySpawnPoint[] enemySpawnPoint;
    [SerializeField] Animation doorAnimation;

    private List<GameObject> currentTrainEnemys = new List<GameObject>();

    private bool isInitialize;
    private bool isClear;

    public override void Init()
    {
        for (int i = 0; i < enemySpawnPoint.Length; i++)
        {
            currentTrainEnemys.Add(Instantiate(enemySpawnPoint[i].enemy, enemySpawnPoint[i].enemyTr.position, Quaternion.identity));
            
            // 임시로 조정한 크기
            currentTrainEnemys[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); 
        }

        isInitialize = true;
    }

    private void Update()
    {
        if (currentTrainEnemys.All(i => i == null) && isInitialize && !isClear)
        {
            ClearStage();
        }
    }

    private void ClearStage()
    {
        doorAnimation.Play();
        isClear = true;
    }
}


[Serializable]
class EnemySpawnPoint
{
    public Transform enemyTr;
    public GameObject enemy;
}