using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text countText;

    [HideInInspector] public Item _item;
    [HideInInspector] public int _count = 1;
    [HideInInspector] public Transform _parentAfterDrag;

    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = newItem.ItemData.ItemImage;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = _count.ToString();
        bool textActive = _count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        countText.raycastTarget = false;
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        // 비활성화 해줘야 인벤토리 슬롯이 비었는지 체크할 수 있음
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        countText.raycastTarget = true;
        transform.SetParent(_parentAfterDrag); 
    }
}
