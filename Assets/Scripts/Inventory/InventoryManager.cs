using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SceneSingleton<InventoryManager>
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject _inventoryItemPrefab;

    [SerializeField] private int _maxStackedItem = 10;         // 겹칠 수 있는 최대 아이템 수

    [SerializeField] private Image selectImg;        // 인벤토리 선택 시 나오는 이미지
    private InventorySlot _selectedSlot;

    [SerializeField] private Button _decompositionBtn;
    [SerializeField] private Button _dropBtn;
    public bool AddItem(Item item)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // 아래 조건에 충족하면 겹칠 수 있기 때문에 아이템 갯수 추가 
            if (itemInSlot != null && itemInSlot._item.Id == item.Id 
                && itemInSlot._count < _maxStackedItem && itemInSlot._item.ItemData.IsStackable == true)
            {
                itemInSlot._count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // 아이템 슬롯이 비어있으면 아이템 추가
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public void SelectSlot(InventorySlot slot)
    {
        selectImg.gameObject.transform.parent.gameObject.SetActive(true);

        selectImg.rectTransform.position = slot.GetComponent<RectTransform>().position;
        _selectedSlot = slot;
    }

    public void DisassembleItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();

        if (item == null) return;

        item._item.Decomposition();
        Destroy(item.gameObject);
    }
    public void DropItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();

        if (item == null) return;
        

    }

}
