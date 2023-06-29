using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public enum Armortype
{
    hat = 0,
    top = 1,
    bottom = 2,
    shoes = 3,
    gloves,
    mask,
    cloak
}

public enum Itemtype
{
    weapon,
    armor,
}

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemData itemData;
    public ItemData ItemData { get { return itemData; } }

    #region Stat
    // ------------------------------------------------ 스탯 ----------------------------------------------------------
    protected string _name;                               
    protected string _description;
    protected float _additionalHp;
    protected int _additionalStemina;
    protected float _additionalStrength;
    protected int _additionalDefense;
    protected float _additionalAttackSpeed;
    protected float _additionalSpeed;
    protected int _durability;

    #endregion

    #region etc
    // ------------------------------------------------ 이외 변수 ----------------------------------------------------------

    protected int _level = 0;                                                       // 아이템 레벨 (0 ~ 4)

    [SerializeField] protected Itemtype _itemType;
    public int Id { get { return itemData.Id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }  
    public float AdditionalHp { get { return _additionalHp; } }
    public int AdditionalStemina { get { return _additionalStemina; } }
    public float AdditionalStrength { get { return _additionalStrength; } }
    public int AdditionalDefense { get { return _additionalDefense; } }
    public float AdditionalAttackSpeed { get { return _additionalAttackSpeed; } }
    public float AdditionalSpeed { get { return _additionalSpeed; } }
    public int Durability { get { return _durability; } }

    public int Level { get { return _level; } }

    public Itemtype ItemType { get { return _itemType; } }

    #endregion
    public Item(Item item)
    {
        itemData = item.ItemData;

        _name = item.Name;
        _description = item.Description;
        _additionalHp = item.AdditionalHp;
        _additionalStemina = item.AdditionalStemina;
        _additionalStrength = item.AdditionalStrength;
        _additionalDefense = item.AdditionalDefense;
        _additionalAttackSpeed  = item.AdditionalAttackSpeed;
        _additionalSpeed = item.AdditionalSpeed;
        _durability = item._durability;
        _level = item.Level;
    }

    public Item(ItemData data)
    {
        this.itemData = data;
        Init();
    }

    public Item()
    {

    }

    /// <summary>
    /// 아이템 정보를 String으로 전환하는 함수
    /// </summary>
    /// <returns>전환된 string을 반환한다.</returns>
    public override string ToString()
    {
        return "Level: " + _level + "   name: " + _name + " hp: " + _additionalHp + "   stemina: " + _additionalStemina + "     str: " + _additionalStrength
            + "     def: " + _additionalDefense + "   AK: " + _additionalAttackSpeed + "      speed: " + _additionalSpeed; 
    }

    protected virtual void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화 하는 함수
    /// </summary>
    protected virtual void Init()
    {
        _name = itemData.Name;
        _description = itemData.Description;
        _additionalHp = itemData.AdditionalHp;
        _additionalStemina = itemData.AdditionalStemina;
        _additionalStrength = itemData.AdditionalStrength;
        _additionalDefense = itemData.AdditionalDefense;
        _additionalAttackSpeed = itemData.AdditionalAttackSpeed;
        _additionalSpeed = itemData.AdditionalSpeed;
        _durability = itemData.MaxDurability;
    }


    /// <summary>
    /// 아이템을 얻었을 때 실행하는 함수
    /// </summary>
    public virtual void EarnItem()
    {

    }

    /// <summary>
    /// 아이템을 장착했을 때 실행하는 함수
    /// </summary>
    public virtual void OnItem()
    {
        UsePassiveSkill();
    }

    /// <summary>
    /// 아이템을 장착 해제했을 때 실행하는 함수
    /// </summary>
    public virtual void OffItem()
    {

    }

    /// <summary>
    /// 패시브스킬을 발동할 때 사용 하는 함수
    /// </summary>
    public virtual void UsePassiveSkill()
    {

    }

    /// <summary>
    /// 아이템을 강화할 때 호출 되는 함수
    /// </summary>
    public virtual void Levelup()
    {
        if (_level >= 1) return;
        
        _level++;
        _additionalHp += itemData.UpgradeValueHp;
        _additionalStemina += itemData.UpgradeValueStemina;
        _additionalStrength += itemData.UpgradeValueStrength;
        _additionalDefense += itemData.UpgradeValueDefense;
        _additionalAttackSpeed += itemData.UpgradeValueAttackSpeed;
        _additionalSpeed += itemData.UpgradeValueSpeed;

        // TODO: 현재 value가 조정되고 플레이어에게 적용되지않음 -> 적용시키는 로직 필요(원래 수치를 참조해 계산하는 방법이라면 필요 X)
    }

    /// <summary>
    /// 내구도를 최대로 회복하는 함수
    /// </summary>
    public void SetDurabilityMax()
    {
        _durability = itemData.MaxDurability;
    }


    /// <summary>
    /// 내구도를 감소시키는 함수
    /// </summary>
    /// <param name="value"></param>
    public void SubDurability(int value)
    {
        _durability -= value;
        if (_durability <= 0)
            Destruction();
    }

    /// <summary>
    /// 아이템 분해함수(직접 분해했을 경우)
    /// </summary>
    public void Decomposition() 
    {
        //3~7개
        int addedGear = UnityEngine.Random.Range(3, 8);
        GearManager.Instance.AddGear(addedGear);
        //TODO: 아이템 포인터 null로 만들기
    }

    /// <summary>
    /// 아이템 파괴함수(공격으로 인해 자연적으로 부숴지는 경우)
    /// </summary>
    public void Destruction()
    {
        //2~5개
        int addedGear = UnityEngine.Random.Range(2, 6);
        GearManager.Instance.AddGear(addedGear);

        //TODO: 아이템 포인터 null로 만들기
    }
}
