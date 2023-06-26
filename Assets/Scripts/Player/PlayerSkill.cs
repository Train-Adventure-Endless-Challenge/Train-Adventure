using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkill : MonoBehaviour
{

    [Header("Transform")]
    [SerializeField] private Transform _weaponTransform; // 무기 위치

    [Header("UI")]
    private int _staminaValue = 7;   // 임시 스태미너 사용값 ※추후 Weapon의 각 사용량을 가져와 처리※

    private Player _player;                     // 플레이어 데이터 담당 클래스
    private Animator _animator;                 // 애니메이션
    private PlayerController _playerController; // 플레이어 움직임 담당 클래스

    private Coroutine _skillCor; // 플레이어 공격 코루틴을 담을 변수


    private void Awake()
    {
        Init(); // 초기화 진행
    }



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
    /// 공격을 담당하는 함수
    /// </summary>
    public void Skill()
    {
        if (CanSkill()) // 공격이 가능한 상태라면 
        {
            _player.Stamina -= _staminaValue;         // 스태미나 감소
            IngameUIController.Instance.UpdateStamina(_player.Stamina, _player._maxStamina);
            _player.playerState = PlayerState.Skill; // 플레이어 상태를 공격 상태로 변경

            _skillCor = StartCoroutine(SkillCor());
        }
    }

    /// <summary>
    /// 공격이 가능한지 체크하는 함수
    /// <br/>
    /// 조건 : 공격 실행중이 아니라면, 마우스를 눌렀다면, 스태미나가 충분하다면
    /// </summary>
    /// <returns></returns>
    private bool CanSkill()
    {
        if (_skillCor == null && Input.GetMouseButtonDown(1)
            && _player.Stamina - _staminaValue >= 0 &&
            PlayerManager.Instance.EquipItem.CurrentWeapon != null
            && !EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }


    public void AttackCollsionOnEvent()
    {
        PlayerManager.Instance.EquipItem.CurrentWeapon.AttackCollisionOn();
    }

    IEnumerator SkillCor()
    {
        Weapon curWeapon = PlayerManager.Instance.EquipItem.CurrentWeapon; // 무기 변경

        if (curWeapon == null) { StopCoroutine(_skillCor); _skillCor = null; }

        _animator.SetBool("IsSkill", true);          // 애니메이션 실행
        _animator.SetTrigger("OnState");              // 애니메이션 상태 변경
        _animator.SetInteger("Weapon", curWeapon.Id); // 무기 종류에 따라 변경
        PlayerManager.Instance.EquipItem.CurrentWeapon.AttackCollisionOn();

        curWeapon.UseActiveSkill(); // 공격 실행

        yield return new WaitForSeconds(1);
        // 플레이어의 속도에 따라 상태를 Idle, Move로 바꿈
        if (_playerController._moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;
        _animator.SetBool("IsSkill", false);          // 애니메이션 실행
        _skillCor = null; // 코루틴 초기화
    }
}