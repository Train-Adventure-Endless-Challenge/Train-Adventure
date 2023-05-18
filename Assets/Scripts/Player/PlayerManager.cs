using UnityEngine;

/// <summary>
/// 플레이어를 관리하는 클래스
/// </summary>
public class PlayerManager : SceneSingleton<PlayerManager>
{
    #region Variable

    public bool isPC = true;       // 테스트용
    public Vector3 inputDirection; // 입력 값

    private Player _player;
    private PlayerController _playerController;
    private PlayerRolling _playerRolling;
    private PlayerAttack _playerAttack;

    #endregion

    #region Function

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
        _playerRolling = GetComponent<PlayerRolling>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        //#if UNITY_EDITOR

        // 플랫폼 전환을 실험하기 위한 테스트 코드 ※추후 삭제※
        if (isPC)
        {
            _playerRolling.Roll(true);
        }
        if (_player.playerState != PlayerState.Rolling)
        {
            if (isPC)
            {
                _playerAttack.Attack(true);
                _playerController.Move();
            }
            else
            {
                _playerController.Move(inputDirection);
            }
        }

        // PC시 실행되어야 하는 코드 ※추후 활성화※
        //_playerRolling.Roll(true);
        //if (_player.playerState != PlayerState.Rolling)
        //{
        //    _playerController.Move();
        //} 

        //#endif

        //#if UNITY_ANDROID        

        // 안드로이드시 실행되어야 하는 코드 ※추후 활성화※
        //if (_player.playerState != PlayerState.Rolling)
        //{
        //    _playerController.Move(inputDirection);
        //}

        //#endif
    }

    /// <summary>
    /// 플랫폼 Switch 버튼을 눌렀을 때ㅇ
    /// </summary>
    public void OnSwitchButton()
    {
        isPC = !isPC;
    }

    /// <summary>
    /// 구르기 버튼을 눌렀을 때
    /// </summary>
    public void OnRollButton()
    {
        if (isPC == false) // ※추후 삭제※
        {
            _playerRolling.Roll(false);
        }
    }
    
    /// <summary>
    /// 공격 버튼을 눌렀을 때
    /// </summary>
    public void OnAttackButton()
    {
        if (isPC == false)
        {
            _playerAttack.Attack(false);
        }
    }

    #endregion
}
