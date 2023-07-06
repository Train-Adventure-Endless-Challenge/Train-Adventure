using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : SceneSingleton<InventoryManager>
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    [SerializeField] private GameObject _inventoryItemPrefab;

    [SerializeField] private int _maxStackedItem = 10;         // 겹칠 수 있는 최대 아이템 수

    [SerializeField] private Image _selectImg;        // 인벤토리 선택 시 나오는 이미지

    [SerializeField] private GameObject _mainInventoryGroup;
    [SerializeField] private GameObject _selectEventObject;   // 아이템 활동 버튼 부모(버리기버튼, 수리)

    [SerializeField] private KeyCode _inventoryKeyCode;


    private InventorySlot _selectedSlot;
    public InventorySlot SelectedSlot { get { return _selectedSlot; } }



    public bool _isDrag;

    private void Update()
    {
        if (Input.GetKeyDown(_inventoryKeyCode))
        {
            if (_isDrag == true) return;

            _mainInventoryGroup.SetActive(_mainInventoryGroup.activeSelf == false);

            _selectedSlot = null;
            _selectEventObject.SetActive(false);
            _selectImg.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 다른 슬롯(강화 인벤토리 UI)에 인벤토리의 아이템을 옮기는 함수
    /// </summary>
    /// <param name="inventorySlots">슬롯들</param>
    public void CopyInventory(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem item = _inventorySlots[i].GetComponentInChildren<InventoryItem>();

            if (item == null) continue;

            item.ParentCanvas = inventorySlots[i].gameObject.GetComponentInParent<Canvas>().gameObject;
            item._slot = inventorySlots[i];
            item.transform.SetParent(inventorySlots[i].transform);
        }
    }

    /// <summary>
    /// 다른 슬롯의 아이템ㅇ르 다시 인벤토리에 옮기는 함수
    /// </summary>
    /// <param name="inventorySlots"></param>

    public void PasteInventory(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem item = inventorySlots[i].GetComponentInChildren<InventoryItem>();

            if (item == null) continue;

            item.ParentCanvas = gameObject;
            item._slot = _inventorySlots[i];
            item.transform.parent = _inventorySlots[i].transform;
        }

    }

    /// <summary>
    /// 지정된 슬롯에 item을 넣는 함수
    /// </summary>
    /// <param name="slot">아이템을 얻을 슬롯</param>
    /// <param name="invenitem">얻을 아이템</param>
    /// <returns></returns>
    public bool AddItem(InventorySlot slot, InventoryItem invenitem)
    {
        if (invenitem == null) return false;

        // 아래 조건에 충족하면 겹칠 수 있기 때문에 아이템 갯수 추가 
        if (invenitem != null && invenitem._item.Id == invenitem._item.Id
            && invenitem._count < _maxStackedItem && invenitem._item.ItemData.IsStackable == true)
        {
            invenitem._count++;
            invenitem.RefreshCount();
            slot.GetComponentInChildren<InventoryItem>()._item.EarnItem();
            return true;
        }

        SpawnNewItem(invenitem._item, slot);
        return true;

    }

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
                slot.GetComponentInChildren<InventoryItem>()._item.EarnItem();
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
        inventoryItem.ParentCanvas = gameObject;
        inventoryItem.InitialiseItem(item);

        inventoryItem._item.EarnItem();
    }

    public void DeleteItem(InventoryItem inventoryItem)
    {
        Destroy(inventoryItem.gameObject);
    }
    public void SelectSlot(InventorySlot slot)
    {
        // 전에 선택하던 item의 raycastTarget 끄기
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();
        if (item != null) item.ItemImage.raycastTarget = false;

        _selectImg.gameObject.SetActive(true); // selectImg, 분해, 드롭 버튼 활성화
        _selectEventObject.SetActive(true);
        _selectImg.gameObject.transform.SetParent(slot.GetComponentInParent<Canvas>().transform);
        _selectImg.rectTransform.position = slot.GetComponent<RectTransform>().position; // selectImg 위치 맞추기

        // 현재 선택하는 item의 raycastTarget 키기
        InventoryItem itemImg = slot?.GetComponentInChildren<InventoryItem>();
        if (itemImg != null) itemImg.ItemImage.raycastTarget = true;

        _selectedSlot = slot;
    }

    public void RepairItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();

        SoundManager.Instance.PlayButtonClickSound(); // 버튼 소리 실행

        if (item == null) return;
        if (GearManager.Instance.GearAmount < 20)
        {
            Debug.Log("기어 부족");
            return;
        }

        item._item.SetDurabilityMax();
        GearManager.Instance.SubGear(20);
    }
    public void DisassembleItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();

        SoundManager.Instance.PlayButtonClickSound(); // 버튼 소리 실행
        if (item == null) return;

        item._slot.TakeItem(ref item); // 장착되어있는 아이템일 경우를 대비해 슬롯에서 item을 뺄 떄 event함수 실행

        item._item.Decomposition();
        Destroy(item.gameObject);
    }

    public void DropItem()
    {
        InventoryItem item = _selectedSlot?.GetComponentInChildren<InventoryItem>();

        SoundManager.Instance.PlayButtonClickSound(); // 버튼 소리 실행

        if (item == null) return;

        item._slot.TakeItem(ref item); // 장착되어있는 아이템일 경우를 대비해 슬롯에서 item을 뺄 떄 event함수 실행

        ItemObject itemObj = Instantiate(ItemDataManager.Instance.ItemObjectPrefab as GameObject, PlayerManager.Instance.transform.position + Vector3.up, Quaternion.identity).GetComponent<ItemObject>();
        itemObj.Init(item._item, isDataReset: false);


        Destroy(item.gameObject);
    }

}
