// 작성자 : 박재만
// 작성일 : 2023-06-18

#region Namespace

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

#endregion

/// <summary>
/// 플레이어의 공격을 담당하는 클래스
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    #region Variable

    [Header("Transform")]
    [SerializeField] private Transform _weaponTransform; // 무기 위치

    [Header("UI")]

    private float _slowSpeedScale;   // 공격시 움직임 감속 배율
    private float _originSpeedScale; // 원래 속도 배율
    private int _staminaValue = 7;   // 임시 스태미너 사용값 ※추후 Weapon의 각 사용량을 가져와 처리※

    #region Class

    private Player _player;                     // 플레이어 데이터 담당 클래스
    private Animator _animator;                 // 애니메이션
    private PlayerController _playerController; // 플레이어 움직임 담당 클래스

    private Coroutine _attackCor; // 플레이어 공격 코루틴을 담을 변수

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 진행
    }

    private void Start()
    {
        DataInit(); // 데이터 초기화 실행
    }

    #endregion

    /// <summary>
    /// 초기화를 담당하는 함수
    /// <br/>
    /// 플레이어, 애니메이션 등 클래스 초기화
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    /// <summary>
    /// 데이터 초기화를 담당하는 함수
    /// <br/>
    /// 움직임 감속 배율, 원래 속도 배율 초기화
    /// <br/>
    /// ※추후 각 무기의 사용 스테미나 초기화 추가※
    /// </summary>
    private void DataInit()
    {
        _slowSpeedScale = _player.AttackSlowSpeedScale; // 움직임 감속 배율 초기화
        _originSpeedScale = _player.OriginSpeedScale;   // 원래 속도 배율 초기화
    }

    /// <summary>
    /// 공격을 담당하는 함수
    /// </summary>
    public void Attack()
    {
        if (CanAttack()) // 공격이 가능한 상태라면 
        {
            _player.Stamina -= _staminaValue;         // 스태미나 감소
            IngameUIController.Instance.UpdateStamina(_player.Stamina, _player._maxStamina);
            _player.playerState = PlayerState.Attack; // 플레이어 상태를 공격 상태로 변경
            _attackCor = StartCoroutine(AttackCor()); // 공격 코루틴 실행
        }
    }

    /// <summary>
    /// 공격이 가능한지 체크하는 함수
    /// <br/>
    /// 조건 : 공격 실행중이 아니라면, 마우스를 눌렀다면, 스태미나가 충분하다면
    /// </summary>
    /// <returns></returns>
    private bool CanAttack()
    {
        if (_attackCor == null && Input.GetMouseButtonDown(0) 
            && _player.Stamina - _staminaValue >= 0 &&
            PlayerManager.Instance.EquipItem.CurrentWeapon != null
            && !EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 공격 코루틴 함수
    /// <br/> 
    /// 애니메이션, 무기 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCor()
    {
        Weapon curWeapon = PlayerManager.Instance.EquipItem.CurrentWeapon; // 무기 변경

        if (curWeapon == null) { StopCoroutine(_attackCor); _attackCor = null; }

        _animator.SetBool("IsAttack", true);          // 애니메이션 실행
        _animator.SetInteger("Weapon", curWeapon.Id); // 무기 종류에 따라 변경
        _animator.SetFloat("AttackSpeed",             // 공격속도에 따라 애니메이션 속도 조정
            curWeapon.DefalutAttackSpeed / curWeapon.AttackSpeed);
        _animator.SetTrigger("OnState");              // 애니메이션 상태 변경


        curWeapon.Attack(); // 공격 실행

        _playerController.ChangeSlowSpeed(_slowSpeedScale, curWeapon.AttackSpeed); // 속도 감속

        yield return new WaitForSeconds(curWeapon.AttackSpeed); // 애니메이션 시간 대기

        _playerController.ChangeSlowSpeed(_originSpeedScale, curWeapon.AttackSpeed); // 공격 멈춤

        _animator.SetBool("IsAttack", false); // 애니메이션 중단
        _animator.SetTrigger("OnState");      // 애니메이셔 상태 변경

        // 플레이어의 속도에 따라 상태를 Idle, Move로 바꿈
        if (_playerController._moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;
        _attackCor = null; // 코루틴 초기화
    }

    public void AttackCollsionOnEvent()
    {
        PlayerManager.Instance.EquipItem.CurrentWeapon.AttackCollisionOn();
    }
    #endregion
}
