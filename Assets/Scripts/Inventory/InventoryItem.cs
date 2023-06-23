using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private Image _image;                  // 아이템 아이콘 이미지
    [SerializeField] private TMP_Text _countText;
    
    [HideInInspector] public Item _item;                   
    [HideInInspector] public int _count = 1;                // 아이템 갯수
    [HideInInspector] public Transform _parentAfterDrag;

    public InventorySlot _slot;
    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = newItem.ItemData.ItemImage;
        RefreshCount();
    }

    public void RefreshCount()
    {
        _countText.text = _count.ToString();

        // 아이템 갯수가 1개 이하이면 텍스트 끄기
        bool isTextActive = _count > 1;
        _countText.gameObject.SetActive(isTextActive);
    }

    /// <summary>
    /// 드래그 시작했을 때 실행되는 함수
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _countText.raycastTarget = false;
        // 비활성화 해줘야 인벤토리 슬롯이 비었는지 체크할 수 있음

        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    /// <summary>
    /// 드래그 중에 실행되는 함수
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// 드래그 끝났을 때 실행되는 함수
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        _countText.raycastTarget = true;
        transform.SetParent(_parentAfterDrag); 
    }
}
