using UnityEngine;

/// <summary>
/// 플레이어를 관리하는 클래스
/// </summary>
public class PlayerManager : SceneSingleton<PlayerManager>
{
    #region Variable

    #region Mobile

    //public bool _isPC = true;       // 테스트용
    //public Vector3 _inputDirection; // 입력 값

    #endregion

    [SerializeField] private Transform _interactionTransform;

    [Header("ShakeDebuff")]
    [SerializeField] private float _rollLimit = 5.0f;        // 구르기 제한 값
    [SerializeField] private int _maxStaminaReduceRatio = 5; // 스태미나 감소 비율
    [SerializeField] private int _dizzinessLimit = 8;        // 어지러움 제한 값

    private Player _player;
    private PlayerController _playerController;
    private PlayerRolling _playerRolling;
    private PlayerAttack _playerAttack;
    private PlayerSkill _playerSkill;
    private PlayerStamina _playerStamina;
    private PlayerInteraction _playerInteraction;
    private PlayerEquip _playerEquip;

    public PlayerEquip EquipItem { get { return _playerEquip; } }
    public bool IsGodMode { get { return _playerRolling._isGodMode; } }
    public bool CanRoll { set { if (value == false) { IngameUIController.Instance.PopupText("흔들림으로 인해 구르기 불가!"); } _canRoll = value; } }

    private bool _inputBlocking;
    private bool _canRoll = true;


    #endregion

    #region Function

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        #region Mobile

        // 플랫폼 전환을 실험하기 위한 테스트 코드 ※추후 삭제※
        //if (_isPC && _player.playerState != PlayerState.Hit)
        //{
        //_playerRolling.Roll(true);
        //if (_isPC)
        //{
        //_playerAttack.Attack(true);
        //}
        //else
        //{
        //    _playerController.Move(_inputDirection);
        //}
        //}
        //#endif

        #endregion

        if (_inputBlocking) return;

        if (_player.playerState == PlayerState.Rolling || _player.playerState == PlayerState.Attack || _player.playerState == PlayerState.Skill)
        {
            _playerStamina.RecoverStop();
        }
        else
        {
            _playerStamina.Recover();
        }

        if (_player.playerState == PlayerState.Dizziness)
        {
            return;
        }

        if (_player.playerState != PlayerState.Hit && _player.playerState != PlayerState.Skill && _canRoll)
        {
            _playerRolling.Roll();
        }

        if (_player.playerState != PlayerState.Rolling && _player.playerState != PlayerState.Hit && _player.playerState != PlayerState.Skill)
        {
            _playerAttack.AttackCheck();
            _playerController.Move();
        }

        if (_player.playerState != PlayerState.Rolling && _player.playerState != PlayerState.Attack && _player.playerState != PlayerState.Hit)
        {
            _playerSkill.Skill();
        }

        _playerInteraction.Interact(); // 상호작용 실행
    }

    private void FixedUpdate()
    {
        _playerController.GroundedCheck();
    }

    private void Init()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
        _playerRolling = GetComponent<PlayerRolling>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerSkill = GetComponent<PlayerSkill>();
        _playerStamina = GetComponent<PlayerStamina>();
        _playerInteraction = _interactionTransform.GetComponent<PlayerInteraction>();
        _playerEquip = GetComponent<PlayerEquip>();
    }

    public void StopMove()
    {
        _playerController.StopMove();
    }

    /// <summary>
    /// 스킬 발동 이벤트 함수
    /// </summary>
    public void SkillEvent()
    {
        EquipItem.CurrentWeapon.SkillEventFunc();
    }
    public void BlockInput(bool value)
    {
        _inputBlocking = value;
    }

    public void UseShakeDebuff(int shakeAmount)
    {
        CanRoll = _rollLimit > shakeAmount;
        _playerStamina.UpdateMaxStamina(shakeAmount * _maxStaminaReduceRatio);
        if (shakeAmount >= _dizzinessLimit)
        {
            _playerController.StartDizziness();
        }
    }

    #region Mobile

    ///// <summary>
    ///// 플랫폼 Switch 버튼을 눌렀을 때
    ///// </summary>
    //public void OnSwitchButton()
    //{
    //    _isPC = !_isPC; // 플랫폼 변경
    //}

    ///// <summary>
    ///// 구르기 버튼을 눌렀을 때
    ///// </summary>
    //public void OnRollButton()
    //{
    //    if (_isPC == false)             // ※추후 삭제※
    //    {
    //        _playerRolling.Roll(false); // 구르기 실행
    //    }
    //}

    ///// <summary>
    ///// 공격 버튼을 눌렀을 때
    ///// </summary>
    //public void OnAttackButton()
    //{
    //    if (_isPC == false)
    //    {
    //        _playerAttack.Attack(false);
    //    }
    //}

    #endregion

    #endregion
}
