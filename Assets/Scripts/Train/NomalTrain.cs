using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NomalTrain : Train
{
    [SerializeField] private EnemySpawnPoint[] _enemySpawnPoints;
    [SerializeField] Animation _frontDoorAnimation;
    [SerializeField] Animation _backDoorAnimation;

    private List<GameObject> _currentTrainEnemys = new List<GameObject>();

    private bool _isInitialize;
    private bool _isClear;

    public override void Init()
    {
        for (int i = 0; i < _enemySpawnPoints.Length; i++)
        {
            _currentTrainEnemys.Add(Instantiate(_enemySpawnPoints[i].enemy, _enemySpawnPoints[i].enemySpawmPointTr.position, Quaternion.identity));
            
            // 임시로 조정한 크기
            _currentTrainEnemys[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f); 
        }

        _isInitialize = true;
    }

    private void Update()
    {
        // 임시로 테스트 하기위한 코드 
        if (_currentTrainEnemys.All(i => i == null) && _isInitialize && !_isClear)
        {
            ClearStage();
        }
    }

    private void ClearStage()
    {
        _frontDoorAnimation.Play();
        //_frontDoorAnimation.clip = _frontDoorAnimation.GetClip("Door_Close");
        _isClear = true;
    }
}


[Serializable]
class EnemySpawnPoint
{
    public Transform enemySpawmPointTr;
    public GameObject enemy;
}