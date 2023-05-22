using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// 이 함수를 지닌 오브젝트 위에서 마우스를 땠을 때
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) // 슬롯이 비어 있으면 슬롯으로 이동
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem._parentAfterDrag = transform;
        }
        else // 슬롯이 비어 있지 않으면 아이템 스왑
        {
            InventoryItem dropInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            Transform temp = dropInventoryItem._parentAfterDrag;
            dropInventoryItem._parentAfterDrag = transform;

            InventoryItem currentSlotInventoryItem = transform.GetComponentInChildren<InventoryItem>();
            currentSlotInventoryItem.gameObject.transform.SetParent(temp);
        }
    }
}
