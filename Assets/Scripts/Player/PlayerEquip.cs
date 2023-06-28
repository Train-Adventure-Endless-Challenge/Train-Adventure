using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerEquip : MonoBehaviour
{

    private Weapon _currentWeapon;                                          
    public Weapon CurrentWeapon { get { return _currentWeapon; } }      // 현재 무기

    private Armor[] _currentArmors;
    public Armor[] CurrentArmors { get { return _currentArmors; } }     // 현재 방어구

    [SerializeField] private Transform _weaponEquipTransform;           // 무기 피봇
    [SerializeField] private Transform[] _armorEquipTransform;          // 방어구 피봇


    private void Start()
    {
        // TEST
        //EquipItem((ItemDataManager.Instance.ItemPrefab[0] as GameObject).GetComponent<Item>());
    }

    /// <summary>
    /// 아이템을 장착하는 함수
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    public bool EquipItem(Item item)
    {

        Object itemPrefab = ItemDataManager.Instance.ItemPrefab[item.Id];
        GameObject itemObj = Instantiate(itemPrefab as GameObject, transform.position, Quaternion.identity);
        
        if (item.ItemType == Itemtype.weapon)
        {
            Weapon weapon = itemObj.GetComponent<Weapon>();
            _currentWeapon = weapon;
            weapon.transform.parent = _weaponEquipTransform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero;
            return true;
        }
        else if(item.ItemType == Itemtype.armor)
        {
            Armor armor = itemObj.GetComponent<Armor>();

            armor.transform.parent = _armorEquipTransform[(int)armor.ArmorType];
            return true;
        }
        return false;
    }

    
    /// <summary>
    /// 장착 해제하는 함수
    /// </summary>
    /// <param name="item">해제할 아이템</param>
    public bool ReleaseItem(Item item)
    {

        if (item.ItemType == Itemtype.weapon)
        {
            Destroy(_currentWeapon.gameObject);
            _currentWeapon = null;
            return true;
        }
        else if (item.ItemType == Itemtype.armor)
        {
            Armor armor = item as Armor;

            Destroy(_currentArmors[(int)armor.ArmorType].gameObject);
            _currentArmors[(int)armor.ArmorType] = null;
            return true;
        }

        return false;
    }
}
