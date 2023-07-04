using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : InteractionObject
{
    public Item _item;
    private bool _isDataReset;                           // 드랍으로 인해  생긴 오브젝트인가
    [SerializeField] private ItemData _itemData;

    private void Start()
    {
        // 상자나 drop으로 얻은 것이아닌 그냥 바닥에 떨어져있을 경우에만 실행하기
        Init(_item, _isDataReset);
    }

    public void Init(Item item, bool isDataReset = false) 
    {
        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[_itemData.Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;
        itemObj.GetComponent<Item>().enabled = false;
        
        itemObj.AddComponent<Outline>().enabled = false;
        
        _isDataReset = isDataReset;
        _item = item;
        _itemData = ItemDataManager.Instance.ItemData[_itemData.Id];
    }


    public override void Interact()
    {
        if (!_isDataReset)
        {
            Object itemPrefab = ItemDataManager.Instance.ItemPrefab[_itemData.Id];
            Item item = itemPrefab.GetComponent<Item>();
            item.UpdateData();
            InventoryManager.Instance.AddItem(item);
        }
        else
        {
            InventoryManager.Instance.AddItem(_item);
        }
        Destroy(gameObject);
    }
}
