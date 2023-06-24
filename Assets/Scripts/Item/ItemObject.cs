using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : InteractionObject
{
    public int Id;

    [SerializeField] private ItemData _itemData;

    void Start()
    {
        GameObject itemObj =
            Instantiate(ItemDataManager.Instance.ItemPrefab[Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;
        itemObj.GetComponent<Item>().enabled = false;

        _itemData = ItemDataManager.Instance.ItemData[Id];
    }


    public override void Interact()
    {
        InventoryManager.Instance.AddItem(new Item(_itemData));
        Destroy(gameObject);
    }
}
