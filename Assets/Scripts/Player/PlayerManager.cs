using UnityEngine;

/// <summary>
/// 플레이어를 관리하는 클래스
/// </summary>
public class PlayerManager : SceneSingleton<PlayerManager>
{
    #region Variable

    //public bool _isPC = true;       // 테스트용
    //public Vector3 _inputDirection; // 입력 값

    private Player _player;
    private PlayerController _playerController;
    private PlayerRolling _playerRolling;
    private PlayerAttack _playerAttack;

    #endregion

    #region Function

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        //#if UNITY_EDITOR

        // 플랫폼 전환을 실험하기 위한 테스트 코드 ※추후 삭제※
        //if (_isPC && _player.playerState != PlayerState.Hit)
        if (_player.playerState != PlayerState.Hit)
        {
            //_playerRolling.Roll(true);
            _playerRolling.Roll();
        }
        if (_player.playerState != PlayerState.Rolling && _player.playerState != PlayerState.Hit)
        {
            //if (_isPC)
            //{
            //_playerAttack.Attack(true);
            _playerAttack.Attack();
            _playerController.Move();
            //}
            //else
            //{
            //    _playerController.Move(_inputDirection);
            //}
        }

        //#endif
    }

    private void Init()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
        _playerRolling = GetComponent<PlayerRolling>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

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
}
