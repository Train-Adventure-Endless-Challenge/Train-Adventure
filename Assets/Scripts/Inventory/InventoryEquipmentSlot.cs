using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentSlot : InventorySlot
{
    [SerializeField] private Itemtype _slotType;
    [SerializeField] private Armortype _armorType;
    public override bool TakeItem(Item item)
    {
        if (PlayerManager.Instance.EquipItem.ReleaseItem(item) == false) return false;

        return true;
    }

    public override bool PutItem(Item item)
    {
        if (item.ItemType != _slotType) return false;

        if(PlayerManager.Instance.EquipItem.EquipItem(item) == false) return false;
        
        return true;
    }
}
