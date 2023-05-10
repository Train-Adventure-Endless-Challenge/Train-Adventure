using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _trainObjects;
    [SerializeField] private Transform _backgroundGroup;

    [SerializeField] private Train _currentTrain;               // ���� ����       
    [SerializeField] private Train _nextTrain;                  // ���� ����

    private Vector3 _trainInterval = new Vector3(0, 0, 1.5f);   // ���� ����
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

    [ContextMenu("!")] // �׽�Ʈ ��
    public void RandomNextStage()
    {
        StartNextTrain(Random.Range(0, 3));
    }

    // ���� ������ �̵����� �� �Լ�
    public void StartNextTrain(int trainIndex)
    {
        _currentTrain.DestroyGameObejct();
        _currentTrain = _nextTrain;
        _currentTrain.Init();

        _nextTrain = CreateTrain(trainIndex, _currentTrain.transform.position);

        // �� ���� ���� ĭ�� �ٴ� ������ ũ��� trainInterval ���� nextPosition�� ����
        Vector3 nextPosition = new Vector3(0, 0, (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _trainInterval;
        
        _nextTrain.transform.position += nextPosition;

        _backgroundGroup.position += new Vector3(0, 0, nextPosition.z);
    }

    // ���� ���� �Լ�
    private Train CreateTrain(int trainIndex, Vector3 position)
    {
        return Instantiate(_trainObjects[trainIndex], position, Quaternion.identity).GetComponent<Train>();
    }

}

