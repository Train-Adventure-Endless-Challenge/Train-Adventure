using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    protected override void Init()
    {
        base.Init();
    }
    public override void Attack()
    {
        base.Attack();
    }

    public override void AttackColliderOnFunc()
    {
        base.AttackColliderOnFunc();
    }

    public override void EarnItem()
    {
        base.EarnItem();
    }

    public override void Levelup()
    {
        base.Levelup();
    }

    public override void OffItem()
    {
        base.OffItem();
    }

    public override void OnItem()
    {
        base.OnItem();
    }

    public override void SkillEventFunc()
    {
        base.SkillEventFunc();
    }

    public override void UpdateData()
    {
        base.UpdateData();
    }

    public override void UpdateData(Item item)
    {
        base.UpdateData(item);
    }

    public override void UpgradeItem()
    {
        base.UpgradeItem();
    }

    public override void UseActiveSkill()
    {
        base.UseActiveSkill();
    }

    public override void UsePassiveSkill()
    {
        base.UsePassiveSkill();
    }
}
