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

    protected attackType _attackType;

    protected int _consumeDurability;
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    void Update()
    {

    }

    protected override void Init()
    {
        base.Init();
        _damage = itemData.Damage;
        _range = itemData.Range;
        _attackSpeed = itemData.AttackSpeed;
        switch (_attackType)
        {
            case attackType.meele:
                _consumeDurability = 2;
                break;
            case attackType.ranged:
                _consumeDurability = 1;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 공격 로직이 들어있는 함수
    /// </summary>
    public virtual void Attack()
    {
        SubDurability(_consumeDurability);
    }

    /// <summary>
    /// 액티브 스킬 로직이 들어있는 함수
    /// </summary>
    public virtual void UseActiveSkill()
    {
        SubDurability(_consumeDurability);
    }
}
