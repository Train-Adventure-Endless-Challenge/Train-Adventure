using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTrain : Train
{
    [SerializeField] private Transform[] _storeItemTransform;
    [SerializeField] private GameObject _storeItemObject;

    private Quaternion _itemRotation = Quaternion.Euler(new Vector3(0, 45, 0));

    public override void Init()
    {
        OpenDoor();

        StoreItemObject storeItemObject =
                Instantiate(_storeItemObject, _storeItemTransform[0].position, _itemRotation).GetComponent<StoreItemObject>();

        storeItemObject.Init(ItemDataManager.Instance.ItemData[3], 30);

        for (int i = 1; i < _storeItemTransform.Length; i++)
        {
            storeItemObject = 
                Instantiate(_storeItemObject, _storeItemTransform[i].position, _itemRotation).GetComponent<StoreItemObject>();
            storeItemObject.Init(ItemDataManager.Instance.ItemData[1], Random.Range(30, 40));
        }
    }
}
