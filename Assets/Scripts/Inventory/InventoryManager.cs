using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int _maxStackedItems = 10;
    public InventorySlot[] _inventorySlots;
    public GameObject _inventoryItemPrefab;

    public bool AddItem(Item item)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot._item.Id == item.Id 
                && itemInSlot._count < _maxStackedItems && itemInSlot._item.ItemData.Stackable == true)
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
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
