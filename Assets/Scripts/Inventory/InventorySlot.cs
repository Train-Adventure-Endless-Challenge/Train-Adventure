using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem._parentAfterDrag = transform;
        }
        else // Swap
        {
            InventoryItem dropInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            Transform temp = dropInventoryItem._parentAfterDrag;
            dropInventoryItem._parentAfterDrag = transform;

            InventoryItem curInventoryItem = transform.GetComponentInChildren<InventoryItem>();
            curInventoryItem.gameObject.transform.SetParent(temp);
        }

    }
}
