using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerSkill : MonoBehaviour
{

    [Header("Transform")]
    [SerializeField] private Transform _weaponTransform; // 무기 위치

    private int _staminaValue => PlayerManager.Instance.EquipItem.CurrentWeapon.ItemData.SkillConsumeStamina;   // Weapon의 각 스태미나 사용량

    private Player _player;                     // 플레이어 데이터 담당 클래스
    private PlayerSound _playerSound;           // 플레이어 소리 담당 클래스
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
        _playerSound = GetComponent<PlayerSound>();
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
            _player.playerState = PlayerState.Skill; // 플레이어 상태를 스킬 상태로 변경

            _playerSound.PlayAttackSound();
            _player.Stamina -= _staminaValue;         // 스태미나 감소
            IngameUIController.Instance.UpdateStaminaUI(_player.Stamina, _player.MaxStamina);
            if (_skillCor == null)
                _skillCor = StartCoroutine(SkillCor());
        }
    }

    /// <summary>
    /// 공격이 가능한지 체크하는 함수
    /// <br/>
    /// 조건 : 공격 실행중이 아니라면, 마우스를 눌렀다면, 스태미나가 충분하다면, UI 클릭이아니라면, 무기의 공격이 가능하다면(쿨타임)
    /// </summary>
    /// <returns></returns>
    private bool CanSkill()
    {
        bool canSkillWithoutStamina = _skillCor == null
            && Input.GetMouseButtonDown(1)
            && PlayerManager.Instance.EquipItem.CurrentWeapon != null
            && !EventSystem.current.IsPointerOverGameObject()
            && PlayerManager.Instance.EquipItem.CurrentWeapon.CanSkill
            && PlayerManager.Instance.EquipItem.CurrentWeapon.isSkillExist;

        if (canSkillWithoutStamina && _player.Stamina - _staminaValue >= 0)
        {
            return true;
        }
        else if (canSkillWithoutStamina)
        {
            IngameUIController.Instance.PopupText("스태미나가 부족합니다.");
        }
        return false;
    }

    /// <summary>
    /// 공격 콜라이더 활성화 이벤트 함수
    /// </summary>
    public void AttackCollsionOnEvent()
    {
        PlayerManager.Instance.EquipItem.CurrentWeapon.AttackCollisionOn();
    }


    IEnumerator SkillCor()
    {
        Weapon curWeapon = PlayerManager.Instance.EquipItem.CurrentWeapon; // 무기 변경

        if (curWeapon == null) { StopCoroutine(_skillCor); _skillCor = null; }

        _animator.SetBool("IsSkill", true);          // 애니메이션 실행
        _animator.SetInteger("Weapon", curWeapon.Id); // 무기 종류에 따라 변경
        _animator.SetTrigger("OnState");              // 애니메이션 상태 변경

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);


        _animator.SetBool("IsSkill", false);          // 애니메이션 실행
        _animator.SetTrigger("OnState");              // 애니메이션 상태 변경

        // 플레이어의 속도에 따라 상태를 Idle, Move로 바꿈
        if (_playerController._moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;

        _skillCor = null;
    }


}
