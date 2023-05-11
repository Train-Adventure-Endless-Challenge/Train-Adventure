using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUpgradeSlot : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        ItemUpgradeSystem.Instance.EquipedItem = ItemUpgradeSystem.Instance.CurrentDragSlot.GetComponent<Item>();
        ItemUpgradeSystem.Instance.CurrentDragSlot.transform.position = transform.position;

        // 테스트 코드, 나중에 추가된 인벤토리 코드에 맞춰 로직 수정 필요
    }

    void Start()
    {
        ItemUpgradeSystem.Instance.UpgradeSlot = this;
    }

    void Update()
    {
        
    }

    
}
