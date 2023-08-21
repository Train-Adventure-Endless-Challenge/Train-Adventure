using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_BossCave_Punch : MonoBehaviour
{
    EnemyController _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<EnemyController>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit(_owner.Damage, _owner.gameObject);
        }

        //카메라 흔들림. 추후 흔들림 메니저 관리 호출로 변경
        PlayerManager.Instance.GetComponent<CinemachineImpulseSource>().GenerateImpulse();

    }
}