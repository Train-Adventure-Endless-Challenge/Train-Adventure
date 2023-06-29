using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        Object itemObj = ItemDataManager.Instance.ItemPrefab[_itemData.Id];
        InventoryManager.Instance.AddItem(itemObj.GetComponent<Item>());

        GearManager.Instance.SubGear(_cost);

        Destroy(gameObject);
    }
}
