using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentSlot : InventorySlot
{
    [SerializeField] private Itemtype _slotType;
    [SerializeField] private Armortype _armorType;
    public override bool TakeItem(ref InventoryItem inventoryItem)
    {
        if (inventoryItem._item.ItemType != _slotType)
        {
            PlayerManager.Instance.EquipItem.EquipItem(ref inventoryItem);
            return false;
        }

        if (PlayerManager.Instance.EquipItem.ReleaseItem(inventoryItem._item) == false) return false;

        return true;
    }

    public override bool PutItem(ref InventoryItem inventoryItem)
    {
        if (inventoryItem._item.ItemType != _slotType)
        {
            PlayerManager.Instance.EquipItem.EquipItem(ref inventoryItem);
            return false;
        }
        if(PlayerManager.Instance.EquipItem.EquipItem(ref inventoryItem) == false) return false;
        
        return true;
    }

}
