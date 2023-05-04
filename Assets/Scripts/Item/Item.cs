using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private string _name;
    private string _description;
    private int _damage; // 무기에만 사용할 것 같음 (무기의 기본 공격력)
    private float _additionalHp;
    private int _additionalStemina;
    private float _additionalStrength;
    private int _additionalDefense;
    private float _additionalAttackSpeed;
    private float _additionalSpeed;


    private void Awake()
    {

    }

    void Init()
    {
        _name = itemData.Name;
        _description = itemData.Description;
        _damage = itemData.Damage;
        _additionalHp = itemData.AdditionalHp;
        _additionalStemina = itemData.AdditionalStemina;
        _additionalStrength = itemData.AdditionalStrength;
        _additionalDefense = itemData.AdditionalDefense;
        _additionalAttackSpeed = itemData.AdditionalAttackSpeed;
        _additionalSpeed = itemData.AdditionalSpeed;
    }


    public virtual void EarnItem()
    {
        PassiveSkill();
    }

    public virtual void PassiveSkill()
    {

    }
}
