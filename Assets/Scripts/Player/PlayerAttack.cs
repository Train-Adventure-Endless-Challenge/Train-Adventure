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

    [Header("Event")]
    public UnityEvent _OnAttackEvent;                      // 플레이어 공격 이벤트
    public UnityEvent _OnStopEvent;                        // 공격 멈춤 이벤트

    private float _animTime = 0.5f;                        // 공격 애니메이션 속도 (원래는 0.44f 초이지만, 오류 방지를 위해 0.06초 추가 하여 사용)

    private Weapon _weapon;                                // 무기 (추후 공격, 스킬을 위해 사용)
    private Player _player;                                // 플레이어
    private Animator _animator;                            // 애니메이션
    private PlayerController _playerController;            // 플레이어 움직임

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
        //_weapon = _weaponTr.GetComponent<Weapon>(); // ※추후 활성화※
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    #region Mobile
    ///// <param name="isPC">플랫폼 테스트용 변수 ※추후 삭제※</param>
    //public void Attack(bool isPC)
    #endregion
    public void Attack()
    {
        if (_attackCor == null)                                 // 코루틴이 실행되고 있지 않을 때
        {
            #region Mobile
            //if ((isPC && Input.GetMouseButtonDown(0)) || !isPC) // PC, 모바일 체크
            #endregion
            if (Input.GetMouseButtonDown(0) && _attackCor == null)
            {
                _player.playerState = PlayerState.Attack;       // 상태 변경
                _attackCor = AttackCor();                       // 코루틴 변수 할당
                StartCoroutine(AttackCor());                    // 공격 코루틴 실행
            }
        }
    }

    /// <summary>
    /// 공격 코루틴 함수
    /// <br/> 애니메이션, trail, State 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCor()
    {
        // 공격 실행
        _animator.SetBool("IsAttack", true);                            // 애니메이션 실행
        _OnAttackEvent.Invoke();                                        // 공격 이벤트 실행
        _playerController.ChangeSlowSpeed(_slowSpeedScale, _animTime);  // 감속 실행

        yield return new WaitForSeconds(_animTime);                     // 애니메이션 시간 대기

        // 공격 멈춤
        _playerController.ChangeSlowSpeed(_originSpeedScale, _animTime); // 감속 해제
        _animator.SetBool("IsAttack", false);                            // 애니메이션 중단
        _OnStopEvent.Invoke();                                           // 멈춤 이벤트 실행

        // 플레이어의 속도에 따라 상태를 Idle, Move로 바꿈
        if (_playerController._moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;
        _attackCor = null;                                               // 코루틴 초기화
    }

    #endregion
}
