using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemUpgradeSystem : MonoBehaviour
{

    #region 임시 싱글톤
    static ItemUpgradeSystem _instance;
    public static ItemUpgradeSystem Instance { get { return _instance; } }
    #endregion

    //--------------------------------------- Test 용 -------------------------------------------------------------------------------------------------------
    public TestItemSlot CurrentDragSlot { get; set; }

    public TMP_Text _debugText;
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    [SerializeField] private InventorySlot _upgradeSlot;
    public InventorySlot UpgradeSlot { get { return _upgradeSlot; } set { _upgradeSlot = value; } }

    public Item EquipedItem { get { 
            if (UpgradeSlot.GetComponentInChildren<InventoryItem>() != null)
                return UpgradeSlot.GetComponentInChildren<InventoryItem>()._item;

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
        // Test => 나중에 삭제
        try
        {
            _debugText.text = EquipedItem.ToString();
        }
        catch
        {
        }
    }

    public void LevelUpItem()
    {
        try
        {
            EquipedItem._levelupEvent.Invoke();
        }
        catch
        {
            Debug.LogError("아이템 없음");

        }
    }
}
