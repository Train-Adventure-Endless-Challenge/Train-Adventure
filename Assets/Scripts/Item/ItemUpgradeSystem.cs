using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemUpgradeSystem : MonoBehaviour
{

    #region 임시 싱글톤
    static ItemUpgradeSystem _instance;
    public static ItemUpgradeSystem Instance { get { return _instance; } }
    #endregion

    [SerializeField] private int _upgradeCost;

    [SerializeField] private InventorySlot _upgradeSlot;
    [SerializeField] GameObject _upgradePanel;
    public InventorySlot UpgradeSlot { get { return _upgradeSlot; } set { _upgradeSlot = value; } }

    [SerializeField] private InventorySlot[] _slots;

    [SerializeField] Animator _upgradeAnim;

    [SerializeField] GameObject _effectPrefab;
    [SerializeField] Transform _effectPos;
    [SerializeField] Transform _weaponOnObjectPos;
    [SerializeField] Transform _spawnWeaponPos;

    private bool isUpgrading;
    public InventoryItem EquipedItem
    {
        get
        {
            if (UpgradeSlot.GetComponentInChildren<InventoryItem>() != null)
                return UpgradeSlot.GetComponentInChildren<InventoryItem>();

            return null;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        _upgradeAnim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
    }

    public void LevelUpItem()
    {
        if (GearManager.Instance.GearAmount < _upgradeCost)
        {
            IngameUIController.Instance.PopupText("기어가 부족합니다.");
            return;
        }
        else if (EquipedItem._item.IsUpgrade)
        {
            IngameUIController.Instance.PopupText("이미 업그레이드된 아이템 입니다.");

            return;
        }

        EquipedItem._item.Levelup();

        GearManager.Instance.SubGear(_upgradeCost);


        Debug.Log("강화 성공");

        StartCoroutine(UpgradeCor());
    }

    IEnumerator UpgradeCor()
    {
        isUpgrading = true;
        // 모루 위 아이템 소환  
        Object prefab = ItemDataManager.Instance.ItemPrefab[EquipedItem._item.ItemData.Id];
        GameObject weaponObj = Instantiate(prefab as GameObject, _weaponOnObjectPos.position, _weaponOnObjectPos.rotation);

        // UI 정리
        InventoryItem _equipitem = EquipedItem; // 삭제 전 아이템 복사

        _upgradePanel.SetActive(false); // UI 비활성화
        InventoryManager.Instance.PasteInventory(_slots); // 인벤 다시 복귀

        _upgradeAnim.SetTrigger("Upgrade");

        yield return new WaitForSeconds(_upgradeAnim.GetCurrentAnimatorStateInfo(0).length);

        Instantiate(_effectPrefab, _effectPos); // 이펙트 소환

        yield return new WaitForSeconds(1);


        Destroy(weaponObj); // 모루 위 아이템 제거
        Destroy(EquipedItem.gameObject); // 강화 슬롯에있던 아이템 제거

        // 강화된 아이템 소환
        prefab = ItemDataManager.Instance.ItemObjectPrefab;
        ItemObject obj = Instantiate(prefab as GameObject, _spawnWeaponPos.position, Quaternion.identity).GetComponent<ItemObject>();
        obj.Init(_equipitem._item, false);

        yield return new WaitForSeconds(1); // 연타하면 아이템이 사라지는 버그가 있어 1초 쉬어주기
        isUpgrading = false;
        
    }
    public void OpenEvent()
    {
        if(isUpgrading) return;

        _upgradePanel.SetActive(true);
        InventoryManager.Instance.CopyInventory(_slots);

        
    }
    public void CloseEvent()
    {
        if (EquipedItem != null)
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                InventorySlot slot = _slots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot == null)
                {
                    InventoryManager.Instance.AddItem(slot, EquipedItem);
                    Destroy(EquipedItem.gameObject);
                    break;
                }
            }
        }

        _upgradePanel.SetActive(false);
        InventoryManager.Instance.PasteInventory(_slots);


    }


}
