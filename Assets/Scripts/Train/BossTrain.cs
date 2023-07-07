using UnityEngine;

public class BossTrain : Train
{
    [SerializeField] private Transform _bossSpawnPoint;

    [Header("Boss")]
    [SerializeField] private GameObject[] _boss;

    [Header("Sound")]
    [SerializeField] private AudioClip _bossBgSound; // 보스 배경음악 클립


    public override void Init()
    {
        SpawnBoss();
        SoundManager.Instance.BgSoundPlay(_bossBgSound);
    }

    private void SpawnBoss()
    {
        int bossIndex = (InGameManager.Instance.Score / 10) - 1;

        Instantiate(_boss[bossIndex], _bossSpawnPoint.position, Quaternion.identity).
            GetComponentInChildren<EnemyController>()._dieEvent += KillBoss;
    }

    public void KillBoss()
    {
        OpenDoor();
    }
}
