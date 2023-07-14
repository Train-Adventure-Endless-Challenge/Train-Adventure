using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTrain : Train
{
    [SerializeField] private Transform[] _storeItemTransform;
    [SerializeField] private GameObject _storeItemObject;


    public override void Init()
    {
        OpenDoor();

        StoreItemObject storeItemObject =
                Instantiate(_storeItemObject, _storeItemTransform[0].position, Quaternion.identity).GetComponent<StoreItemObject>();

        storeItemObject.Init(ItemDataManager.Instance.ItemData[3], 30);

        for (int i = 1; i < _storeItemTransform.Length; i++)
        {
            storeItemObject = 
                Instantiate(_storeItemObject, _storeItemTransform[i].position, Quaternion.identity).GetComponent<StoreItemObject>();
            storeItemObject.Init(ItemDataManager.Instance.ItemData[1], Random.Range(30, 40));
        }
    }
}
