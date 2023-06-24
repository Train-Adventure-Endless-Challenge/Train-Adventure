using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentSlot : InventorySlot
{
    [SerializeField] private Itemtype _slotType;
    [SerializeField] private Armortype _armorType;
    public override bool TakeItem(Item item)
    {
        PlayerManager.Instance.EquipItem.ReleaseItem(item);

        return true;
    }

    public override bool PutItem(Item item)
    {
        if (item.ItemType != _slotType) return false;

        PlayerManager.Instance.EquipItem.EquipItem(item);
        
        return true;
    }
}
