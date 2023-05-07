using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _trainObjects;
    [SerializeField] private Transform _backgroundGroup;

    private Vector3 _trainPositionOffset;
    private int _currentTrain;

    private void Start()
    {
        _trainPositionOffset = _trainObjects[1].transform.position - _trainObjects[0].transform.position;
        _currentTrain = 0;
    }

    [ContextMenu("!")] // 테스트 용
    public void StartNextStage()
    {
        _trainObjects[_currentTrain].transform.position += (_trainPositionOffset * 2);
        _backgroundGroup.position += _trainPositionOffset;
        _currentTrain ^= 1;
    }

}
