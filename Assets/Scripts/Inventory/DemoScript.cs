using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 테스트용 스크립트
public class DemoScript : MonoBehaviour
{
    public InventoryManager _inventoryManager;
    public Item[] _items;

    public void PickupItem(int id)
    {   
        bool isResult = _inventoryManager.AddItem(new Item(_items[id]));
        if (isResult == true)
        {
        }
        else
        {
        }
    }
}
