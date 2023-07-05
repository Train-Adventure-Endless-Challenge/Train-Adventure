using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Melee : MonoBehaviour
{
    EnemyController_Melee _enemyController;

    private void Awake()
    {
        _enemyController = GetComponentInParent<EnemyController_Melee>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_enemyController._isAttackCheck || _enemyController._isDie) return;          // 현재 공격 상태가 아니라면 또는 죽었다면 return

        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_enemyController.Damage, _enemyController.gameObject);
        }
    }
}
