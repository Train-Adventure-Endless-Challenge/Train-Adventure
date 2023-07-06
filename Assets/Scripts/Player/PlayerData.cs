// 작성자 : 박재만
// 작성일 : 2023-06-18

using UnityEngine;

/// <summary>
/// 플레이어의 데이터를 담고 있는 ScriptableObject 상속 클래스
/// </summary>
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue - 10)]
public class PlayerData : ScriptableObject
{
    #region Variable

    #region Stat

    [Header("Stat")]
    [SerializeField] private float _hp;    // 체력
    [SerializeField] private float _speed; // 속도
    [SerializeField] private int _defense; // 방어력
    [SerializeField] private int _stamina; // 스태미나

    #endregion

    #region Attack

    [Header("Attack")]
    [SerializeField] private float _originSpeedScale;     // 원래 속도 배율
    [SerializeField] private float _attackSlowSpeedScale; // 공격시 움직임 감속 배율

    #endregion

    #region Controller

    [Header("Controller")]
    [SerializeField] private float _walkSpeedScale;     // 걷기 속도 배율
    [SerializeField] private float _runSpeedScale;      // 달리기 속도 배율
    [SerializeField] private float _moveSlowSpeedScale; // 상태 변경시, 움직임 감속 배율

    #endregion

    #region Interaction

    [Header("Interaction")]
    [SerializeField] private float _interactionAngle; // 상호작용 각도
    [SerializeField] private float _interactionRange; // 상호작용 범위
    [SerializeField] private Color _interactionColor; // 에디터 상에서 보여질 상호작용 범위 색깔

    #endregion

    #region Rolling

    [Header("Rolling")]
    [SerializeField] private float _rollingRange;          // 구르기 범위
    [SerializeField] private int _staminaUseValue;         // 구르기 사용 스태미나
    [SerializeField] private AnimationCurve _rollingCurve; // 구르기 변화 커브

    #endregion

    #region Stamina

    [Header("Stamina")]
    [SerializeField] private float _waitTime;     // 변화 대기 시간
    [SerializeField] private float _recoveryTime; // 회복 시간
    [SerializeField] private int _maxValue;       // 스테미너 최대값
    [SerializeField] private int _recoveryValue;  // 회복 시간당 회복량

    #endregion

    #region Sound

    [Header("Sound")]
    [SerializeField] private AudioClip _attackSound;       // 일반 공격 효과음
    [SerializeField] private AudioClip _attackWeaponSound; // 무기 공격 효과음
    [SerializeField] private AudioClip _skillSound;        // 스킬 효과음

    [SerializeField] private AudioClip[] _hitSounds;  // 피격 효과음

    #endregion

    #region Property

    #region Stat

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public int Defense { get { return _defense; } }
    public int Stamina { get { return _stamina; } }

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
    public AudioClip WeaponAttackSound { get { return _attackWeaponSound; } }
    public AudioClip SkillSound { get { return _skillSound; } }

    public AudioClip[] HitSounds { get { return _hitSounds; } }

    #endregion

    #endregion

    #endregion
}
