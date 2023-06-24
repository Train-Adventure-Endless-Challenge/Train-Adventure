using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SceneSingleton<InventoryManager>
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject _inventoryItemPrefab;

    [SerializeField] private int _maxStackedItem = 10;         // 겹칠 수 있는 최대 아이템 수

    [SerializeField] private Image selectImg;        // 인벤토리 선택 시 나오는 이미지
    private InventorySlot _selectedSlot;
    public InventorySlot SelectedSlot {  get { return _selectedSlot; } }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // 아래 조건에 충족하면 겹칠 수 있기 때문에 아이템 갯수 추가 
            if (itemInSlot != null && itemInSlot._item.Id == item.Id 
                && itemInSlot._count < _maxStackedItem && itemInSlot._item.ItemData.IsStackable == true)
            {
                itemInSlot._count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            // 아이템 슬롯이 비어있으면 아이템 추가
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(_inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem._slot = slot;
        inventoryItem.InitialiseItem(item);
    }

    public void SelectSlot(InventorySlot slot)
    {
        // 전에 선택하던 item의 raycastTarget 끄기
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();
        if(item != null) item.ItemImage.raycastTarget = false;
        
        selectImg.gameObject.transform.parent.gameObject.SetActive(true); // selectImg, 분해, 드롭 버튼 활성화
        selectImg.rectTransform.position = slot.GetComponent<RectTransform>().position; // selectImg 위치 맞추기

        // 현재 선택하는 item의 raycastTarget 키기
        InventoryItem itemImg = slot?.GetComponentInChildren<InventoryItem>();
        if (itemImg != null) itemImg.ItemImage.raycastTarget = true;
        
        _selectedSlot = slot;
    }

    public void DisassembleItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();
        if (item == null) return;

        item._slot.TakeItem(item._item); // 장착되어있는 아이템일 경우를 대비해 슬롯에서 item을 뺄 떄 event함수 실행

        item._item.Decomposition();
        Destroy(item.gameObject);
    }
    public void DropItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();
        if (item == null) return;

        item._slot.TakeItem(item._item); // 장착되어있는 아이템일 경우를 대비해 슬롯에서 item을 뺄 떄 event함수 실행

        ItemObject itemObj = Instantiate(ItemDataManager.Instance.ItemObjectPrefab as GameObject, PlayerManager.Instance.transform.position + Vector3.up, Quaternion.identity).GetComponent<ItemObject>();
        itemObj.Id = item._item.Id;
        Destroy(item.gameObject);
    }

}
