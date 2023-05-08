using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    protected int _damage;
    protected float _range;
    protected float _attackSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        _damage = itemData.Damage;
        _range = itemData.Range;
        _attackSpeed = itemData.AttackSpeed;
    }

    /// <summary>
    /// ���� ������ ����ִ� �Լ�
    /// </summary>
    public virtual void Attack()
    {
        
    }

    /// <summary>
    /// ��Ƽ�� ��ų ������ ����ִ� �Լ�
    /// </summary>
    public virtual void UseActiveSkill()
    {

    }

}
