using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrain : Train
{
    [SerializeField] private Transform _bossSpawnPoint;

    [Header("Boss")]
    [SerializeField] private GameObject[] _boss;


    public override void Init()
    {
        SpawnBoss();
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
