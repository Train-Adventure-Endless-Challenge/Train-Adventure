using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _trainObjects;
    [SerializeField] private Transform _backgroundGroup;

    private Train _currentTrain;
    private Train _nextTrain;
    private Vector3 _trainPositionOffset;

    private void Start()
    {   
        _trainPositionOffset = _trainObjects[1].transform.position - _trainObjects[0].transform.position;
    }

    [ContextMenu("!")] // 테스트 용
    public void StartNextStage()
    {   

    }

}
