using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUpgradeSystem : MonoBehaviour
{

    #region ¿”Ω√ ΩÃ±€≈Ê
    static ItemUpgradeSystem _instance;
    public static ItemUpgradeSystem Instance { get { return _instance; } }
    #endregion

    //--------------------------------------- Test øÎ -------------------------------------------------------------------------------------------------------
    public TestItemSlot CurrentDragSlot { get; set; }

    public TMP_Text _debugText;
    // -------------------------------------------------------------------------------------------------------------------------------------------------
    private ItemUpgradeSlot _upgradeSlot;
    public ItemUpgradeSlot UpgradeSlot { get { return _upgradeSlot; } set { _upgradeSlot = value; } }

    private Item _equipedItem;
    public Item EquipedItem { get { return _equipedItem; } set { _equipedItem = value; } }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if(_equipedItem != null)
        {
            _debugText.text = EquipedItem.ToString();
        } 
    }

    public void ItemLevelUp()
    {
        _equipedItem.Levelup();
    }
}
