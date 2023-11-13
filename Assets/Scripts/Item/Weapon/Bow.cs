using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{

    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _arrowSpawnPos;
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

    [ContextMenu("AA")]
    public override void AttackColliderOnFunc()
    {
        Vector3 spawnPos = _arrowSpawnPos.position;
        Arrow arrow = Instantiate(_arrowPrefab, spawnPos, Quaternion.identity).GetComponent<Arrow>();
        Vector3 dest = ItemModel.transform.position;
        Vector3 forward = ItemModel.transform.forward;
        dest += new Vector3(ItemModel.transform.forward.x, 0, ItemModel.transform.forward.z);
        arrow._destPos = dest;
        arrow._damage = _damage;
        arrow.Owner = gameObject;
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
