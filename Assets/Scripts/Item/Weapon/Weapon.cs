using Cinemachine;
using System.Collections;
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
    protected float _defalutAttackSpeed;
    protected float _skillCoolTime;

    protected Transform _playerTransform;
    protected attackType _attackType;
    [SerializeField] protected CapsuleCollider _weaponCollider;
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float DefalutAttackSpeed { get { return _defalutAttackSpeed; } }

    public float currentCoolTime;

    protected GameObject _hittingFeelingEffect;

    private CinemachineImpulseSource _shakeImpulse;

    public Coroutine _hitStopCor;


    public bool isSkillExist;
    public bool CanSkill
    {
        get { return currentCoolTime <= Time.time; }
    }

    [SerializeField] protected GameObject _upgradeEffectPrefab;
    [SerializeField] protected Transform _upgradeEffectTransform;

    protected bool _isSkill;

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
        _defalutAttackSpeed = _attackSpeed;
        _skillCoolTime = itemData.SkillCooltime;
        _hittingFeelingEffect = itemData.HittingFeelingEffect;
        _shakeImpulse = GetComponent<CinemachineImpulseSource>();
    }

    /// <summary>
    /// 애니메이션 이벤트로 판정 콜라이더를 활성화하기위한 함수
    /// </summary>
    public void AttackCollisionOn()
    {
        if(_weaponCollider != null)
            _weaponCollider.enabled = true;

        AttackColliderOnFunc();
    }

    /// <summary>
    /// 공격으로 인해 collider가 on 됐을 때 실행되는 이벤트
    /// </summary>
    public virtual void AttackColliderOnFunc()
    {

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
        SubDurability(itemData.SkillConsumeDurability);
        currentCoolTime = Time.time + _skillCoolTime;
        IngameUIController.Instance.UpdateSkillUI(Id, ItemData.SkillCooltime, currentCoolTime);

    }

    public virtual void SkillEventFunc()
    {

    }
    protected void Shake()
    {
        _shakeImpulse.GenerateImpulse();
    }

    public void HitStop(float duration)
    {
        Time.timeScale = 0;
        if (_hitStopCor == null) _hitStopCor = StartCoroutine(HitStopCor(duration));

    }
    IEnumerator HitStopCor(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;

        _hitStopCor = null;
    }

    public override void UpgradeItem()
    {
        base.UpgradeItem();
        GameObject obj = Instantiate(_upgradeEffectPrefab, _upgradeEffectTransform);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
    }
}
