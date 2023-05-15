using UnityEngine;

/// <summary>
/// 플레이어를 관리하는 클래스
/// </summary>
public class PlayerManager : SceneSingleton<PlayerManager>
{
    #region Variable

    public bool isPC = true; // 테스트용
    public Vector3 inputDirection;

    private Player _player;
    private PlayerController _playerController;
    private PlayerRolling _playerRolling;

    #endregion

    #region Function

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
        _playerRolling = GetComponent<PlayerRolling>();
    }

    private void Update()
    {
        //#if UNITY_EDITOR

        if (isPC)
        {
            _playerRolling.Roll(true);
        }
        if (_player.playerState != PlayerState.Rolling)
        {
            if (isPC)
            {
                _playerController.Move();
            }
            else
            {
                _playerController.Move(inputDirection);
            }
        }

        //#endif

        //#if UNITY_ANDROID        

        //#endif
    }

    public void OnSwitchButton()
    {
        isPC = !isPC;
    }

    public void OnRollButton()
    {
        if (isPC == false)
            _playerRolling.Roll(false);
    }

    #endregion
}
