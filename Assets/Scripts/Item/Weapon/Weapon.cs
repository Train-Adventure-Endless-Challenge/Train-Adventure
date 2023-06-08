using UnityEngine;

public enum attackType
{
    melee,
    ranged,
}


public class Weapon : Item
{
    protected int _damage;
    protected float _range;
    protected float _attackSpeed;
    protected Transform _playerTransform;
    protected attackType _attackType;

    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    public float currentCoolTime;
    void Update()
    {

    }

    private void Start()
    {
        _playerTransform = PlayerManager.Instance.transform;
    }

    protected override void Init()
    {
        base.Init();
        _damage = itemData.Damage;
        _range = itemData.Range;
        _attackSpeed = itemData.AttackSpeed;
    }

    /// <summary>
    /// 공격 로직이 들어있는 함수
    /// </summary>
    public virtual void Attack()
    {
        SubDurability(itemData.AttackConsumeDurability);
    }

    /// <summary>
    /// 액티브 스킬 로직이 들어있는 함수
    /// </summary>
    public virtual void UseActiveSkill()
    {
        SubDurability(itemData.SkillConsumeDurability);
        currentCoolTime = Time.time + itemData.SkillCooltime;
    }
}
