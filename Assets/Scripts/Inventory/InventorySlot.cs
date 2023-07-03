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
            if (inventoryItem._slot.TakeItem(ref inventoryItem) == false || PutItem(ref inventoryItem) == false)
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

            if (this.TakeItem(ref currentSlotInventoryItem) == false
                || prev_slot.TakeItem(ref dropInventoryItem) == false)
                dropInventoryItem.gameObject.transform.SetParent(prev_slot.transform);

            if (prev_slot.PutItem(ref currentSlotInventoryItem) == false
                        || this.PutItem(ref dropInventoryItem) == false)
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
    /// <param name="inventoryItem">놓일 아이템UI 스크립트</param>
    /// <returns></returns>
    public virtual bool PutItem(ref InventoryItem inventoryItem) // 장비를 장착할 때 새로운 item 객체가 생성되는데 그것을 적용시켜주기위해 주솟값을 넘겨줌
    {
        return true;
    }

    /// <summary>
    /// 슬롯에서 아이템을 가져갔을 때 실행하는 이벤트 함수
    /// </summary>
    public virtual bool TakeItem(ref InventoryItem inventoryItem)
    {
        return true;
    }
}
