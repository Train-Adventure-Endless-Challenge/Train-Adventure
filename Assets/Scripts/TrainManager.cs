using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _trainObjects;
    [SerializeField] private Transform _backgroundGroup;

    [SerializeField] private Train _currentTrain;
    [SerializeField] private Train _nextTrain;
    private Vector3 _traininterval = new Vector3(0, 0, 1.5f);
    private Vector3 _startPosition = Vector3.zero;


    private void Start()
    {
        _currentTrain = Instantiate(_trainObjects[0], _startPosition, Quaternion.identity)
            .GetComponent<Train>();
        _currentTrain.Init();

        _nextTrain = Instantiate(_trainObjects[0], _startPosition, Quaternion.identity)
            .GetComponent<Train>();

        _nextTrain.transform.position += new Vector3(0,0,
            (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _traininterval;
    }


    [ContextMenu("!")] // 테스트 용
    public void RandomNextStage()
    {
        StartNextStage(Random.Range(0, 3));
    }


    public void StartNextStage(int trainIndex)
    {
        _currentTrain.DestroyGameObejct();
        _currentTrain = _nextTrain;
        _currentTrain.Init();

        _nextTrain = Instantiate(_trainObjects[trainIndex], _currentTrain.transform.position, Quaternion.identity)
            .GetComponent<Train>();

        Vector3 nextPosition = new Vector3(0,0,(_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _traininterval;

        _nextTrain.transform.position += nextPosition;

        _backgroundGroup.position += new Vector3(0, 0, nextPosition.z);
    }

}
