using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    private Weapon _currentWeapon;
    public Weapon CurrentWeapon { get { return _currentWeapon; } }      // 현재 무기

    [SerializeField] private Weapon _fistObject;

    private Armor[] _currentArmors;
    public Armor[] CurrentArmors { get { return _currentArmors; } }     // 현재 방어구

    [SerializeField] private Transform _weaponEquipTransform;           // 무기 피봇
    [SerializeField] private Transform[] _armorEquipTransform;          // 방어구 피봇

    public bool IsFist { get { return _fistObject.gameObject.activeSelf; } }

    private void Start()
    {
        FistActivate(true);
    }

    /// <summary>
    /// 아이템을 장착하는 함수
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    public bool EquipItem(ref InventoryItem itemInventory)
    {
        Object itemPrefab = ItemDataManager.Instance.ItemPrefab[itemInventory._item.Id];
        GameObject itemObj = Instantiate(itemPrefab as GameObject, transform.position, Quaternion.identity);

        if (itemInventory._item.ItemType == Itemtype.weapon)
        {
            FistActivate(false);

            Weapon weapon = itemObj.GetComponent<Weapon>();
            _currentWeapon = weapon;
            weapon.transform.parent = _weaponEquipTransform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localEulerAngles = Vector3.zero;
            weapon.UpdateData(itemInventory._item);

            itemInventory._item = weapon;
            itemInventory._item.InventoryItem = itemInventory;

            // 스킬 쿨타임 재 설정
            weapon.currentCoolTime = Time.time + weapon.ItemData.SkillCooltime;
            IngameUIController.Instance.UpdateSkillUI(CurrentWeapon.Id, CurrentWeapon.ItemData.SkillCooltime, CurrentWeapon.currentCoolTime);
            IngameUIController.Instance.UpdateDurabilityUI(weapon.ItemData.MaxDurability, weapon.Durability);
            IngameUIController.Instance.OnDurabilityUI(true);

            return true;
        }
        else if (itemInventory._item.ItemType == Itemtype.armor)
        {
            Armor armor = itemObj.GetComponent<Armor>();

            armor.transform.parent = _armorEquipTransform[(int)armor.ArmorType];

            itemInventory._item = armor;
            itemInventory._item.InventoryItem = itemInventory;

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
            if (_currentWeapon == _fistObject) return false;

            IngameUIController.Instance.OnDurabilityUI(false);

            Destroy(_currentWeapon.gameObject);
            FistActivate(true);
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

    public void FistActivate(bool on)
    {
        _fistObject.gameObject.SetActive(on);
        if (on)
        {
            _currentWeapon = _fistObject;
            IngameUIController.Instance.UpdateSkillUI(CurrentWeapon.Id, CurrentWeapon.ItemData.SkillCooltime, CurrentWeapon.currentCoolTime);
        }
        else
        {
            _currentWeapon = null;
        }

    }
}
