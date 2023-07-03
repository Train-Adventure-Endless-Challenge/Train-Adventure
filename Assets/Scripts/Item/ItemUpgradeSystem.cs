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
    public InventorySlot UpgradeSlot { get { return _upgradeSlot; } set { _upgradeSlot = value; } }

    [SerializeField] private InventorySlot[] _slots;

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

        EquipedItem._item.Levelup();
        GearManager.Instance.SubGear(_upgradeCost);
        Debug.Log("강화 성공");
    }

    public void OpenEvent()
    {
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

        InventoryManager.Instance.PasteInventory(_slots);


    }


}
