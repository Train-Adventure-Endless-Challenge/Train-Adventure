using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrainManager : MonoBehaviour
{
    [SerializeField] private GameObject[] trainObjects;

    private Vector3 trainPositionOffset;
    private int currentTrain;

    private void Start()
    {
        trainPositionOffset = trainObjects[1].transform.position - trainObjects[0].transform.position;
        currentTrain = 0;
    }

    [ContextMenu("!")]
    public void StartNextStage()
    {
        trainObjects[currentTrain].transform.position += (trainPositionOffset * 2);   
        currentTrain ^= 1;
    }

}
