using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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


    // ------------------------------------------------ 스탯 ----------------------------------------------------------
    protected string _name;                               
    protected string _description;
    protected float _additionalHp;
    protected int _additionalStemina;
    protected float _additionalStrength;
    protected int _additionalDefense;
    protected float _additionalAttackSpeed;
    protected float _additionalSpeed;
    // ------------------------------------------------ 이외 변수 ----------------------------------------------------------
    protected int _level = 0;                                                       // 아이템 레벨 (0 ~ 4)

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
        if (_level >= 4) return;
        
        _level++;
        _additionalHp += itemData.UpgradeValueHp;
        _additionalStemina += itemData.UpgradeValueStemina;
        _additionalStrength += itemData.UpgradeValueStrength;
        _additionalDefense += itemData.UpgradeValueDefense;
        _additionalAttackSpeed += itemData.UpgradeValueAttackSpeed;
        _additionalSpeed += itemData.UpgradeValueSpeed;

        if(_level == 4)
        {
            // TODO: 추가 옵션 뽑기 및 적용 
        }
        // TODO: 현재 value가 조정되고 플레이어에게 적용되지않음 -> 적용시키는 로직 필요
    }
}
