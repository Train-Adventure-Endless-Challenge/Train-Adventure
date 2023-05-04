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
    shoes
}

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;


    // ------------------------------------------------ 스탯 ----------------------------------------------------------
    private string _name;                               
    private string _description;
    private int _damage;
    private float _range;
    private float _additionalHp;
    private int _additionalStemina;
    private float _additionalStrength;
    private int _additionalDefense;
    private float _additionalAttackSpeed;
    private float _additionalSpeed;

    // ------------------------------------------------ 이외 변수 ----------------------------------------------------------
    private int _level = 0;                                                       // 아이템 레벨 (0 ~ 4)


    private void Awake()
    {

    }

    void Init()
    {
        _name = itemData.Name;
        _description = itemData.Description;
        _damage = itemData.Damage;
        _range = itemData.Range;
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
}
