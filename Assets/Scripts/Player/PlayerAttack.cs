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

    private float _attackSpeed;      // 공격 속도
    private float _slowSpeedScale;   // 공격시 움직임 감속 배율
    private float _originSpeedScale; // 원래 속도 배율
    private int _staminaValue => PlayerManager.Instance.EquipItem.CurrentWeapon.ItemData.AttackConsumeStamina;   // Weapon의 각 스태미나 사용량

    #region Class

    private Player _player;                     // 플레이어 데이터 담당 클래스
    private PlayerSound _playerSound;           // 플레이어 소리 담당 클래스
    private Animator _animator;                 // 애니메이션
    private PlayerController _playerController; // 플레이어 움직임 담당 클래스

    private Coroutine _attackCor;   // 플레이어 공격 코루틴을 담을 변수

    private int _layerMask;

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
        _playerSound = GetComponent<PlayerSound>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();

        _layerMask = (-1) - (1 << LayerMask.NameToLayer("RightWall"));
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
    public void AttackCheck()
    {
        if (CanAttack()) // 공격이 가능한 상태라면 
        {
            _player.Stamina -= _staminaValue;              // 스태미나 감소
            IngameUIController.Instance.UpdateStaminaUI(_player.Stamina, _player.MaxStamina);
            _player.playerState = PlayerState.Attack;      // 플레이어 상태를 공격 상태로 변경
            RotateMouseDirection();                        // 마우스 방향으로 회전하는 함수
            _attackCor = StartCoroutine(AttackCor());      // 공격 코루틴 실행 
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
        bool canAttackWithoutStamina =
            _attackCor == null
            && Input.GetMouseButtonDown(0)
            && !EventSystem.current.IsPointerOverGameObject();


        if (_player.Stamina - _staminaValue >= 0 && canAttackWithoutStamina)
        {
            return true;
        }
        else if (canAttackWithoutStamina)
        {
            IngameUIController.Instance.PopupText("스태미나가 부족합니다.");
        }
        return false;
    }

    /// <summary>
    /// 플레이어 공격시 마우스 방향으로 회전하는 함수
    /// <br/>
    /// Ray를 사용하여 마우스 위치를 받아와 사용
    /// </summary>
    private void RotateMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;

        // 특정 layer만 raycast제외하기 (RightWall)
        if (Physics.Raycast(ray, out hitResult, 100, _layerMask))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
            transform.forward = mouseDir;
        }
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

        _playerSound.PlayAttackSound();

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

        yield return new WaitForSeconds(curWeapon.AttackSpeed * 0.2f); // 애니메이션 시간 대기

        _attackCor = null; // 코루틴 초기화
    }

    public void AttackCollsionOnEvent()
    {
        PlayerManager.Instance.EquipItem.CurrentWeapon.AttackCollisionOn();
    }
    #endregion
}
