using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : InteractionObject
{
    public Item _item;
    private bool _isDrop;                           // 드랍으로 인해  생긴 오브젝트인가
    [SerializeField] private ItemData _itemData;

    private void Start()
    {
        // 상자나 drop으로 얻은 것이아닌 그냥 바닥에 떨어져있을 경우에만 실행하기
        Init(_itemData.Id, _item, _isDrop);
    }

    public void Init(int id, Item item, bool isDrop) 
    {
        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;
        itemObj.GetComponent<Item>().enabled = false;
        
        itemObj.AddComponent<Outline>().enabled = false;
        
        _isDrop = isDrop;
        _item = item;
        _itemData = ItemDataManager.Instance.ItemData[id];
    }


    public override void Interact()
    {
        if (!_isDrop)
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
