// 작성자 : 박재만
// 작성일 : 2023-06-19

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
    [SerializeField] private PlayerData playerData;

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

    private float _hp;    // 체력
    private float _maxHp;
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

    #region Property

    #region Stat

    public float Hp { get { return _hp; } set { _hp = value;} }
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

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 실행
    }

    #endregion

    /// <summary>
    /// 데이터를 초기화 하는 함수
    /// </summary>
    private void Init()
    {
        #region Stat

        _hp = playerData.Hp;           // 체력
        _maxHp = _hp;
        _speed = playerData.Speed;     // 속도
        _defense = playerData.Defense; // 방어력
        _stamina = playerData.Stamina; // 스태미나
        _maxStamina = 100;

        #endregion

        #region Attack

        _originSpeedScale = playerData.OriginSpeedScale;         // 원래 속도 배율
        _attackSlowSpeedScale = playerData.AttackSlowSpeedScale; // 공격시 움직임 감속 배율

        #endregion

        #region Controller

        _walkSpeedScale = playerData.WalkSpeedScale;         // 걷기 속도 배율
        _runSpeedScale = playerData.RunSpeedScale;           // 달리기 속도 배율
        _moveSlowSpeedScale = playerData.MoveSlowSpeedScale; // 상태 변경시, 움직임 감속 배율

        #endregion

        #region Interaction

        _interactionAngle = playerData.InteractionAngle; // 상호작용 각도
        _interactionRange = playerData.InteractionRange; // 상호작용 범위
        _interactionColor = playerData.InteractionColor; // 에디터 상에서 보여질 상호작용 범위 

        #endregion

        #region Rolling

        _rollingRange = playerData.RollingRange;       // 구르기 범위
        _staminaUseValue = playerData.StaminaUseValue; // 구르기 사용 스태미나
        _rollingCurve = playerData.RollingCurve;       // 구르기 변화 커브

        #endregion

        #region Stamina

        _waitTime = playerData.WaitTime;           // 변화 대기 시간
        _recoveryTime = playerData.RecoveryTime;   // 회복 시간
        _maxValue = playerData.MaxValue;           // 스테미너 최대값
        _recoveryValue = playerData.RecoveryValue; // 회복 시간당 회복량

        #endregion

        _playerHit = GetComponent<PlayerHit>();
        _playerDie = GetComponent<PlayerDie>();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        if (PlayerManager.Instance.IsGodMode) return;

        _playerHit.Hit(damage);
        IngameUIController.Instance.UpdateHp(_hp, _maxHp);
    }

    public override void Die()
    {
        _playerDie.Die(); // 플레이어 죽음 실행
    }

    #endregion
}
