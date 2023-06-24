using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    /// <summary>
    /// 이 함수를 지닌 오브젝트 위에서 마우스를 땠을 때
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if (transform.childCount == 0) // 슬롯이 비어 있으면 슬롯으로 이동
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem._parentAfterDrag = transform;
            if(inventoryItem._slot != null)
                inventoryItem._slot.TakeItem(inventoryItem._item);
            
            inventoryItem._slot = this;
            PutItem(inventoryItem._item);
        }
        else // 슬롯이 비어 있지 않으면 아이템 스왑
        {
            InventoryItem dropInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot prev_slot = dropInventoryItem._slot;

            prev_slot.TakeItem(dropInventoryItem._item);
            dropInventoryItem._slot = this;
            this.PutItem(dropInventoryItem._item);

            InventoryItem currentSlotInventoryItem = transform.GetComponentInChildren<InventoryItem>();
            
            this.TakeItem(currentSlotInventoryItem._item);
            currentSlotInventoryItem.gameObject.transform.SetParent(prev_slot.transform);
            prev_slot.PutItem(currentSlotInventoryItem._item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.SelectSlot(this);
    }

    /// <summary>
    /// 슬롯에 아이템을 놓았을 때 실행하는 이벤트 함수
    /// </summary>
    public virtual bool PutItem(Item item)
    {
        return false;
    }

    /// <summary>
    /// 슬롯에서 아이템을 가져갔을 때 실행하는 이벤트 함수
    /// </summary>
    public virtual bool TakeItem(Item item)
    {
        return false;
    }
}
