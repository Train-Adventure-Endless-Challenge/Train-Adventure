// 작성자 : 박재만
// 작성일 : 2023-07-04

#region Namespace

using UnityEngine;

#endregion

/// <summary>
/// 플레이어의 데이터를 담고 있는 클래스
/// </summary>
public class Player : Entity
{
    #region Variable

    #region Data

    /// <summary>
    /// 스크립터블 오브젝트로 저장 되어 있는 플레이어 데이터 원본 값
    /// </summary>
    [SerializeField] private PlayerData _playerData;

    #endregion

    #region Class

    private PlayerHit _playerHit; // 플레이어 히트 담당 클래스
    private PlayerDie _playerDie; // 플레이어 죽음 담당 클래스

    /// <summary>
    /// 플레이어의 상태를 저장하는 Enum 값
    /// </summary>
    public PlayerState playerState;

    #endregion

    #region Stat

    private float _speed; // 속도
    private int _defense; // 방어력
    private int _stamina; // 스태미나
    public int _maxStamina;

    #endregion

    #region Attack

    private float _originSpeedScale;     // 원래 속도 배율
    private float _attackSlowSpeedScale; // 공격시 움직임 감속 배율

    #endregion

    #region Controller

    private float _walkSpeedScale;     // 걷기 속도 배율
    private float _runSpeedScale;      // 달리기 속도 배율
    private float _moveSlowSpeedScale; // 상태 변경시, 움직임 감속 배율

    #endregion

    #region Interaction

    private float _interactionAngle; // 상호작용 각도
    private float _interactionRange; // 상호작용 범위
    private Color _interactionColor; // 에디터 상에서 보여질 상호작용 범위 

    #endregion

    #region Rolling

    private float _rollingRange;          // 구르기 범위
    private int _staminaUseValue;         // 구르기 사용 스태미나
    private AnimationCurve _rollingCurve; // 구르기 변화 커브

    #endregion

    #region Stamina

    private float _waitTime;     // 변화 대기 시간
    private float _recoveryTime; // 회복 시간
    private int _maxValue;       // 스테미너 최대값
    private int _recoveryValue;  // 회복 시간당 회복량

    #endregion

    #region Sound

    private AudioClip _attackSound;       // 일반 공격 효과음
    private AudioClip _weaponAttackSound; // 무기 공격 효과음
    private AudioClip _skillSound;        // 스킬 효과음

    private AudioClip[] _hitSounds; // 피격 효과음

    #endregion

    #region Property

    #region Stat

    public float Speed { get { return _speed; } set { _speed = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public int Stamina { get { return _stamina; } set { _stamina = value; } }

    #endregion

    #region Attack

    public float OriginSpeedScale { get { return _originSpeedScale; } }
    public float AttackSlowSpeedScale { get { return _attackSlowSpeedScale; } }

    #endregion

    #region Controller

    public float WalkSpeedScale { get { return _walkSpeedScale; } }
    public float RunSpeedScale { get { return _runSpeedScale; } }
    public float MoveSlowSpeedScale { get { return _moveSlowSpeedScale; } }

    #endregion

    #region Interaction

    public float InteractionAngle { get { return _interactionAngle; } }
    public float InteractionRange { get { return _interactionRange; } }
    public Color InteractionColor { get { return _interactionColor; } }

    #endregion

    #region Rolling

    public float RollingRange { get { return _rollingRange; } }
    public int StaminaUseValue { get { return _staminaUseValue; } }
    public AnimationCurve RollingCurve { get { return _rollingCurve; } }

    #endregion

    #region Stamina

    public float WaitTime { get { return _waitTime; } }
    public float RecoveryTime { get { return _recoveryTime; } }
    public int MaxValue { get { return _maxValue; } }
    public int RecoveryValue { get { return _recoveryValue; } }

    #endregion

    #region Sound

    public AudioClip AttackSound { get { return _attackSound; } }
    public AudioClip WeaponAttackSound { get { return _weaponAttackSound; } }
    public AudioClip SkillSound { get { return _skillSound; } }

    public AudioClip[] HitSounds { get { return _hitSounds; } }

    #endregion

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 실행
        base.Start();

    }

    #endregion

    /// <summary>
    /// 데이터를 초기화 하는 함수
    /// </summary>
    private void Init()
    {
        #region Stat

        _hp = _playerData.Hp;           // 체력
        _speed = _playerData.Speed;     // 속도
        _defense = _playerData.Defense; // 방어력
        _stamina = _playerData.Stamina; // 스태미나
        _maxStamina = 100;

        #endregion

        #region Attack

        _originSpeedScale = _playerData.OriginSpeedScale;         // 원래 속도 배율
        _attackSlowSpeedScale = _playerData.AttackSlowSpeedScale; // 공격시 움직임 감속 배율

        #endregion

        #region Controller

        _walkSpeedScale = _playerData.WalkSpeedScale;         // 걷기 속도 배율
        _runSpeedScale = _playerData.RunSpeedScale;           // 달리기 속도 배율
        _moveSlowSpeedScale = _playerData.MoveSlowSpeedScale; // 상태 변경시, 움직임 감속 배율

        #endregion

        #region Interaction

        _interactionAngle = _playerData.InteractionAngle; // 상호작용 각도
        _interactionRange = _playerData.InteractionRange; // 상호작용 범위
        _interactionColor = _playerData.InteractionColor; // 에디터 상에서 보여질 상호작용 범위 

        #endregion

        #region Rolling

        _rollingRange = _playerData.RollingRange;       // 구르기 범위
        _staminaUseValue = _playerData.StaminaUseValue; // 구르기 사용 스태미나
        _rollingCurve = _playerData.RollingCurve;       // 구르기 변화 커브

        #endregion

        #region Stamina

        _waitTime = _playerData.WaitTime;           // 변화 대기 시간
        _recoveryTime = _playerData.RecoveryTime;   // 회복 시간
        _maxValue = _playerData.MaxValue;           // 스테미너 최대값
        _recoveryValue = _playerData.RecoveryValue; // 회복 시간당 회복량

        #endregion

        #region Sound

        _attackSound = _playerData.AttackSound;             // 일반 공격 효과음
        _weaponAttackSound = _playerData.WeaponAttackSound; // 무기 공격 효과음
        _skillSound = _playerData.SkillSound;               // 스킬 효과음

        _hitSounds = _playerData.HitSounds; // 피격 효과음

        #endregion

        _playerHit = GetComponent<PlayerHit>();
        _playerDie = GetComponent<PlayerDie>();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        if (PlayerManager.Instance.IsGodMode) return;

        SoundManager.Instance.SFXPlay(_hitSounds[Random.Range(0, HitSounds.Length)]);

        _playerHit.Hit(damage);
        IngameUIController.Instance.UpdateHp(_hp, MaxHp);
    }

    public void Heal(float healAmount)
    {
        _hp += healAmount;
        IngameUIController.Instance.UpdateHp(_hp, MaxHp);
    }

    public override void Die()
    {
        _playerDie.Die(); // 플레이어 죽음 실행
    }

    #endregion
}
