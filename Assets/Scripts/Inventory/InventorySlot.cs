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
        if (transform.childCount == 0) // 슬롯이 비어 있으면 슬롯으로 이동
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (inventoryItem == null) return;
            if (inventoryItem._slot.TakeItem(inventoryItem._item) == false || PutItem(inventoryItem._item) == false)
            {
                inventoryItem.transform.SetParent(inventoryItem._parentAfterDrag);
                return;
            }

            inventoryItem._slot = this;
            inventoryItem._parentAfterDrag = transform;

        }
        else // 슬롯이 비어 있지 않으면 아이템 스왑
        {
            InventoryItem dropInventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot prev_slot = dropInventoryItem._slot;

            InventoryItem currentSlotInventoryItem = transform.GetComponentInChildren<InventoryItem>();

            Transform temp_transform = dropInventoryItem._parentAfterDrag;

            if (this.TakeItem(currentSlotInventoryItem._item) == false
                || prev_slot.PutItem(currentSlotInventoryItem._item) == false)
                dropInventoryItem.gameObject.transform.SetParent(prev_slot.transform);

            if (prev_slot.TakeItem(dropInventoryItem._item) == false
                        || this.PutItem(dropInventoryItem._item) == false)
                dropInventoryItem.gameObject.transform.SetParent(prev_slot.transform);


            dropInventoryItem._parentAfterDrag = transform;
            currentSlotInventoryItem.gameObject.transform.SetParent(temp_transform);

            currentSlotInventoryItem._slot = prev_slot;
            dropInventoryItem._slot = this;


        }
    }

    /// <summary>
    /// slot이 클릭되었을 때 실행되는 event함수
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.SelectSlot(this);
    }

    /// <summary>
    /// 슬롯에 아이템을 놓았을 때 실행하는 이벤트 함수
    /// </summary>
    public virtual bool PutItem(Item item)
    {
        return true;
    }

    /// <summary>
    /// 슬롯에서 아이템을 가져갔을 때 실행하는 이벤트 함수
    /// </summary>
    public virtual bool TakeItem(Item item)
    {
        return true;
    }
}
