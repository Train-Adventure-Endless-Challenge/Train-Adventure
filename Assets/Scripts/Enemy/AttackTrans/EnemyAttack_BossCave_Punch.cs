using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        EnemyController_Boss_Cave enemy = _owner.GetComponent<EnemyController_Boss_Cave>();
        GameObject go = Instantiate(enemy._floorEffect, new Vector3(transform.position.x, other.transform.position.y + 1f, transform.position.z), Quaternion.identity);
        Destroy(go, 1f);

    }
}