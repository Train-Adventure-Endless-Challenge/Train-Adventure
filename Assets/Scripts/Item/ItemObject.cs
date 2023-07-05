using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : InteractionObject
{
    public Item _item;
    private bool _isDataReset;                           // 드랍으로 인해  생긴 오브젝트인가
    [SerializeField] private ItemData _itemData;

    [SerializeField] private bool isFieldDrop;                                // 상자등으로 얻는게아닌 필드드랍인가?
    private void Start()
    {
        // 상자나 drop으로 얻은 것이아닌 그냥 바닥에 떨어져있을 경우에만 실행하기
        if(isFieldDrop == true)
        {
            _item = new Item(_itemData);
            Init(_item, _isDataReset);
        }
    }

    private void Update()
    {
        Object itemPrefab = ItemDataManager.Instance.ItemPrefab[_itemData.Id];
        Item item = itemPrefab.GetComponent<Item>();
        Debug.Log("item:        " + item.ToString());
    }
    public void Init(Item item, bool isDataReset = false) 
    {
        GameObject itemObj =
        Instantiate(ItemDataManager.Instance.ItemPrefab[item.Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;

        Item tempItem = itemObj.GetComponent<Item>();

        if(isDataReset == false) // 혹시라도 드랍, 강화로 인해 아이템 데이터가 바뀐 상황이라면
            tempItem.UpdateData(item);  // 아이템 업데이트

        tempItem.enabled = false;
        itemObj.AddComponent<Outline>().enabled = false;








        _isDataReset = isDataReset;
        _item = item;
        _itemData = ItemDataManager.Instance.ItemData[item.Id];
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
