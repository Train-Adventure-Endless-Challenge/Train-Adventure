using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainObjectInTitle : MonoBehaviour
{
    [SerializeField] Transform _startTrans;
    [SerializeField] Transform _endTrans;

    [SerializeField] GameObject[] _transObjs;       // 기차 오브젝트들

    [SerializeField] private float _speed;
    void Update()
    {
        foreach(GameObject obj in _transObjs)           // title Scene 기차 움직임
        {
            if(Vector3.Distance(obj.transform.position, _endTrans.position) <= 2)
            {
                obj.transform.position = _startTrans.position;
            }
            obj.transform.Translate(Vector3.forward  * Time.deltaTime * _speed);
        }
    }
}
