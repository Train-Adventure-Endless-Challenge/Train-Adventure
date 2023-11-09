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
    public void Init(ItemData itemData)
    {
        _itemData = itemData;

        _cost = itemData.StoreCost;
        _costText.text = $"{_cost} Gear";

        GameObject itemObj =
           Instantiate(ItemDataManager.Instance.ItemPrefab[itemData.Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;

        itemObj.AddComponent<Outline>().enabled = false;

        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public override void Interact()
    {
        if (GearManager.Instance.GearAmount < _cost)
        {
            IngameUIController.Instance.PopupText("기어가 부족합니다.");
            // 나중에 UI로 구현
            return;
        }

        Object itemObj = ItemDataManager.Instance.ItemPrefab[_itemData.Id];
        Item item = itemObj.GetComponent<Item>();
        item.UpdateData();
        InventoryManager.Instance.AddItem(item);

        GearManager.Instance.SubGear(_cost);

        Destroy(gameObject);
    }
}
