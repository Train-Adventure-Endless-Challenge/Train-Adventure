using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager _inventoryManager;
    public Item[] _itemToPickup;

    public void PickupItem(int id)
    {   
        bool result = _inventoryManager.AddItem(new Item(_itemToPickup[id]));
        if (result == true)
        {
            Debug.Log("ITEM ADDED");
        }
        else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }
}
