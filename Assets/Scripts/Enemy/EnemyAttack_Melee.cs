using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Melee : MonoBehaviour
{
    EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponentInParent<EnemyController>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_enemyController._isCurrentAttackCor) return;

        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_enemyController.Damage, _enemyController.gameObject);
        }
    }
}
