using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestItemSlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{


    public void OnBeginDrag(PointerEventData eventData)
    {
        // IF 
        //ItemUpgradeSystem.Instance.CurrentDragSlot = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //ItemUpgradeSystem.Instance.CurrentDragSlot = null;
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.ItemData.ItemImage;
    }

    void Update()
    {
        
    }
}
