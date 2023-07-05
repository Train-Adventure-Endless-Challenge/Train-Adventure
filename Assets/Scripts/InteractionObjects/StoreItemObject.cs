using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreItemObject : InteractionObject
{
    [SerializeField] private int _cost;
    [SerializeField] private ItemData _itemData;

    [Header("UI")]
    [SerializeField] private TMP_Text _costText; 
    public void Init(ItemData itemData, int cost)
    {
        _itemData = itemData;

        _cost = cost;
        _costText.text = $"{cost} Gear";

        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[itemData.Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;

        itemObj.AddComponent<Outline>().enabled = false;
    }

    public override void Interact()
    {
        if (GearManager.Instance.GearAmount < _cost)
        {
            IngameUIController.Instance.PopupText("No Gear");
            // 나중에 UI로 구현
            return;
        }

        Object itemObj = ItemDataManager.Instance.ItemPrefab[_itemData.Id];
        InventoryManager.Instance.AddItem(itemObj.GetComponent<Item>());

        GearManager.Instance.SubGear(_cost);

        Destroy(gameObject);
    }
}
