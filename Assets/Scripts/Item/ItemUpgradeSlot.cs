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

        // �׽�Ʈ �ڵ�, ���߿� �߰��� �κ��丮 �ڵ忡 ���� ���� ���� �ʿ�
    }

    void Start()
    {
        ItemUpgradeSystem.Instance.UpgradeSlot = this;
    }

    void Update()
    {
        
    }

    
}
