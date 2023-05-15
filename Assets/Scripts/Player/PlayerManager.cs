using UnityEngine;

/// <summary>
/// 플레이어를 관리하는 클래스
/// </summary>
public class PlayerManager : SceneSingleton<PlayerManager>
{
    #region Variable

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
#if UNITY_EDITOR

        _playerRolling.Roll();
        if (_player.playerState != PlayerState.Rolling)
        {
            _playerController.Move();
        }

#endif
    }

    public void Move(Vector2 inputDirection)
    {
        _playerController.Move(inputDirection);
    }


#endregion
}
