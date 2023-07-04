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


    private Weapon _currentWeapon;

    [SerializeField] Animation _upgradeAnim;

    [SerializeField] GameObject _effectPrefab;
    [SerializeField] Transform _effectPos;
    [SerializeField] Transform _weaponOnObject;
    [SerializeField] Transform _spawnWeaponPos;


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
        _upgradeAnim = GetComponentInChildren<Animation>();
    }
    private void Update()
    {
    }

    public void LevelUpItem()
    {
        if (GearManager.Instance.GearAmount < _upgradeCost)
        {
            Debug.Log("기어 부족");
            return;
        }
        else if (EquipedItem._item.IsUpgrade)
        {
            Debug.Log("이미 업그레이드된 아이템 입니다.");
            return;
        }

        EquipedItem._item.Levelup();

        GearManager.Instance.SubGear(_upgradeCost);


        Debug.Log("강화 성공");

        StartCoroutine(UpgradeCor());
    }

    IEnumerator UpgradeCor()
    {
        CloseEvent();

        _upgradeAnim.Play();

        yield return new WaitForSeconds(_upgradeAnim.clip.length);

        Instantiate(_effectPrefab, _effectPos);

        yield return new WaitForSeconds(1);

        Object prefab = ItemDataManager.Instance.ItemObjectPrefab;

        ItemObject obj = Instantiate(prefab as GameObject, _spawnWeaponPos.position, Quaternion.identity).GetComponent<ItemObject>();
        obj.Init(EquipedItem._item, false);

        if (EquipedItem != null)
        {
            Destroy(EquipedItem.gameObject);
        }
    }
    public void OpenEvent()
    {
        InventoryManager.Instance.CopyInventory(_slots);
    }
    public void CloseEvent()
    {
        /*if (EquipedItem != null)
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
*/
        _upgradePanel.SetActive(false);
        InventoryManager.Instance.PasteInventory(_slots);


    }


}
