public class Weapon : Item
{
    protected int _damage;
    protected float _range;
    protected float _attackSpeed;

    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

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
    /// 공격 로직이 들어있는 함수
    /// </summary>
    public virtual void Attack()
    {

    }

    /// <summary>
    /// 액티브 스킬 로직이 들어있는 함수
    /// </summary>
    public virtual void UseActiveSkill()
    {

    }
}
