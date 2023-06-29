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

    public InventorySlot _slot;                             // 현재 들어가 있는 slots


    public Image ItemImage { get { return _image; } }

    /// <summary>
    /// 초기화 함수
    public void InitialiseItem(Item newItem)
    {
        _item = newItem;
        _item.InventoryItem = this;
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
        if (_slot != InventoryManager.Instance.SelectedSlot) return;

        _image.raycastTarget = false;
        _countText.raycastTarget = false;
        // 비활성화 해줘야 인벤토리 슬롯이 비었는지 체크할 수 있음

        _parentAfterDrag = transform.parent;
        transform.SetParent(InventoryManager.Instance.transform);

        InventoryManager.Instance._isDrag = true;
    }

    /// <summary>
    /// 드래그 중에 실행되는 함수
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (_slot != InventoryManager.Instance.SelectedSlot) return;

        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// 드래그 끝났을 때 실행되는 함수
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_slot != InventoryManager.Instance.SelectedSlot) return;

        InventoryManager.Instance.SelectSlot(_slot); // 아이템을 옮겼을 때 옮긴쪽으로 Select 이동
        _image.raycastTarget = true;
        _countText.raycastTarget = true;
        transform.SetParent(_parentAfterDrag);
        InventoryManager.Instance._isDrag = false;
    }
}
