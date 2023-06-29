using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemObject : InteractionObject
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private int cost;


    private void Start()
    {
        Init();   
    }

    public void Init()
    {
        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[_itemData.Id] as GameObject, transform.position, Quaternion.identity);
    }

    public override void Interact()
    {

    }
}
