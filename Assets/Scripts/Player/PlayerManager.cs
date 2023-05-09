using UnityEngine;

public class PlayerManager : SceneSingleton<PlayerManager>
{
    private Player _player;
    private PlayerController _playerController;
    private PlayerRolling _playerRolling;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerController = GetComponent<PlayerController>();
        _playerRolling = GetComponent<PlayerRolling>();
    }

    private void Update()
    {
        _playerRolling.Roll();
        if (_player.playerState != PlayerState.Rolling)
        {
            _playerController.Move();
        }
    }
}
