using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// 플레이어의 공격을 담당하는 클래스
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _slowSpeedScale = 0.3f; // 감속 속도 배율 (0.3f)
    [SerializeField] private float _originSpeedScale = 1f; // 원래 속도      (1)
    [SerializeField] private int _staminaValue = 7;        // ※추후 변경※ 임시 스태미너 값

    [Header("Transform")]
    [SerializeField] private Transform _weaponTr;          // 무기 위치

    [Header("Event")]
    public UnityEvent _OnAttackEvent;                      // 플레이어 공격 이벤트
    public UnityEvent _OnStopEvent;                        // 공격 멈춤 이벤트

    [SerializeField] private StaminaSlider _staminaSlider;

    private Player _player;                                // 플레이어
    private Animator _animator;                            // 애니메이션
    private PlayerController _playerController;            // 플레이어 움직임
    private WeaponController _weaponController;

    private IEnumerator _attackCor;                        // 플레이어 공격 코루틴을 담을 변수

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 진행
    }

    #endregion

    /// <summary>
    /// 초기화를 담당하는 함수
    /// <br/>
    /// 플레이어, 애니메이션 등 초기화
    /// </summary>
    private void Init()
    {
        // 플레이어, 애니메이션 등 초기화
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _weaponController = GetComponent<WeaponController>();
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    public void Attack()
    {
        if (_attackCor == null && CanAttack()) // 코루틴이 실행되고 있지 않거나 스태미너가 충분하다면
        {
            if (Input.GetMouseButtonDown(0))
            {
                _player.Stamina -= _staminaValue;
                _staminaSlider.ChangeUI(_player.Stamina);
                _player.playerState = PlayerState.Attack;  // 상태 
                StartCoroutine(AttackCor());               // 공격 코루틴 실행
            }
        }
    }

    /// <summary>
    /// 공격이 가능한지 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool CanAttack()
    {
        return _player.Stamina - _staminaValue >= 0; // 스태미나가 충분한지 체크
    }

    /// <summary>
    /// 공격 코루틴 함수
    /// <br/> 애니메이션, trail, State 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCor()
    {
        Weapon curWeapon = _weaponController._currentWeapon;

        _attackCor = AttackCor();                                       // 코루틴 변수 할당
        // 공격 실행
        _animator.SetBool("IsAttack", true);                            // 애니메이션 실행
        _animator.SetTrigger("OnState");
        _animator.SetInteger("Weapon", curWeapon.Id);
        _animator.SetFloat("AttackSpeed", curWeapon.DefalutAttackSpeed / curWeapon.AttackSpeed);
        // 공격속도에 따라 애니메이션 속도 조정

        curWeapon.Attack();

        _playerController.ChangeSlowSpeed(_slowSpeedScale,
            curWeapon.AttackSpeed);                                     // 감속 실행

        yield return new WaitForSeconds(curWeapon.AttackSpeed);         // 애니메이션 시간 대기

        // 공격 멈춤
        _playerController.ChangeSlowSpeed(_originSpeedScale,
            curWeapon.AttackSpeed);                                      // 감속 해제

        _animator.SetBool("IsAttack", false);                            // 애니메이션 중단
        _animator.SetTrigger("OnState");

        // 플레이어의 속도에 따라 상태를 Idle, Move로 바꿈
        if (_playerController._moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;
        _attackCor = null;                                               // 코루틴 초기화
    }

    #endregion
}
