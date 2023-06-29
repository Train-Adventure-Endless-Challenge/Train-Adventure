using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemObject : InteractionObject
{
    [SerializeField] private int _cost;
    [SerializeField] private ItemData _itemData;

    public void Init(ItemData itemData, int cost)
    {
        _itemData = itemData;

        _cost = cost;

        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[itemData.Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;

        itemObj.AddComponent<Outline>().enabled = false;
    }

    public override void Interact()
    {
        if (GearManager.Instance.GearAmount < _cost)
        {
            Debug.Log("기어가 없습니다.");
            // 나중에 UI로 구현
            return;
        }

        InventoryManager.Instance.AddItem(new Item(_itemData));

        GearManager.Instance.SubGear(_cost);

        Destroy(gameObject);
    }
}
