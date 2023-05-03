using UnityEngine;

public class PlayerManager : SceneSingleton<PlayerManager>
{
    private Player player;
    private PlayerController playerMove;
    private PlayerRolling playerRolling;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerController>();
        playerRolling = GetComponent<PlayerRolling>();
    }

    private void Update()
    {
        playerMove.Move();
    }
}
