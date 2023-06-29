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

        storeItemObject.Init(ItemDataManager.Instance.ItemData[2], Random.Range(1,5));

        for (int i = 1; i < _storeItemTransform.Length; i++)
        {
            storeItemObject = 
                Instantiate(_storeItemObject, _storeItemTransform[i].position, Quaternion.identity).GetComponent<StoreItemObject>();
            storeItemObject.Init(ItemDataManager.Instance.ItemData[0], Random.Range(50, 60));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameManager.Instance.NextStage();
        }
    }
}
