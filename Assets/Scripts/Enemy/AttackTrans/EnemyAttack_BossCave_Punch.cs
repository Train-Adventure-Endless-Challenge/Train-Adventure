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
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit(_owner.Damage, _owner.gameObject);
        }
        
        if(other.gameObject.CompareTag("Floor"))        // 바닥일 시 effect 생성
        {
            EnemyController_Boss_Cave enemy = _owner.GetComponent<EnemyController_Boss_Cave>();
             Instantiate(enemy._floorEffect,new Vector3(transform.position.x,other.transform.position.y,transform.position.z),Quaternion.identity);
        }
    }
}
