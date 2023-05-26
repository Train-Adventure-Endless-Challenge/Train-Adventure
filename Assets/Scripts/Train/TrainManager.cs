using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : SceneSingleton<TrainManager>
{
    [SerializeField] private GameObject[] _trainObjects;
    [SerializeField] private Transform _backgroundGroup;

    [SerializeField] private Train _currentTrain;               // 현재 기차       
    [SerializeField] private Train _nextTrain;                  // 다음 기차

    private Vector3 _trainInterval = new Vector3(0, 0, 1.5f);   // 기차 간격
    private Vector3 _startPosition = Vector3.zero;           
    
    private void Start()
    {
        _currentTrain = CreateTrain(0, _startPosition);
        _currentTrain.Init();

        _nextTrain = CreateTrain(0, _startPosition);

        _nextTrain.transform.position += new Vector3(0, 0,
            (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _trainInterval;
    }

    [ContextMenu("!")] // 테스트 용
    public void RandomNextStage()
    {
        StartNextTrain(Random.Range(0, 3));
    }

    // 다음 기차로 이동했을 때 함수
    public void StartNextTrain(int trainIndex)
    {
        _currentTrain.DestroyGameObejct();
        _currentTrain = _nextTrain;
        _currentTrain.Init();

        _nextTrain = CreateTrain(trainIndex, _currentTrain.transform.position);

        // 두 개의 기차 칸의 바닥 절반의 크기와 trainInterval 더해 nextPosition을 구함
        Vector3 nextPosition = new Vector3(0, 0, (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _trainInterval;
        
        _nextTrain.transform.position += nextPosition;

        _backgroundGroup.position += new Vector3(0, 0, nextPosition.z);
    }

    // 기차 생성 함수
    private Train CreateTrain(int trainIndex, Vector3 position)
    {
        return Instantiate(_trainObjects[trainIndex], position, Quaternion.identity).GetComponent<Train>();
    }

}

