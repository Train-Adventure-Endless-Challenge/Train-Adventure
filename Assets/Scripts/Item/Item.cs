using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ArmorType
{
    hat,
    top,
    bottom,
    shoes,
    gloves,
    mask,
    cloak
}

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemData itemData;
    public ItemData ItemData { get { return itemData; } }

    // ------------------------------------------------ 스탯 ----------------------------------------------------------
    protected int _id;
    protected string _name;                               
    protected string _description;
    protected float _additionalHp;
    protected int _additionalStemina;
    protected float _additionalStrength;
    protected int _additionalDefense;
    protected float _additionalAttackSpeed;
    protected float _additionalSpeed;
    protected int _durability;
    // ------------------------------------------------ 이외 변수 ----------------------------------------------------------
    protected int _level = 0;                                                       // 아이템 레벨 (0 ~ 4)

    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }  
    public float AdditionalHp { get { return _additionalHp; } }
    public int AdditionalStemina { get { return _additionalStemina; } }
    public float AdditionalStrength { get { return _additionalStrength; } }
    public int AdditionalDefense { get { return _additionalDefense; } }
    public float AdditionalAttackSpeed { get { return _additionalAttackSpeed; } }
    public float AdditionalSpeed { get { return _additionalSpeed; } }


    public int Level { get { return _level; } }

    public UnityEvent _levelupEvent;

    public Item(Item item)
    {
        itemData = item.ItemData;

        _id = item.Id;
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
        _id = itemData.Id;
        _name = itemData.Name;
        _description = itemData.Description;
        _additionalHp = itemData.AdditionalHp;
        _additionalStemina = itemData.AdditionalStemina;
        _additionalStrength = itemData.AdditionalStrength;
        _additionalDefense = itemData.AdditionalDefense;
        _additionalAttackSpeed = itemData.AdditionalAttackSpeed;
        _additionalSpeed = itemData.AdditionalSpeed;
        _durability = itemData.MaxDurability;
        _levelupEvent.AddListener(Levelup);
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
    protected virtual void Levelup()
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
}
