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

        if (!_enemyController._isAttackCheck || _enemyController._isDie ||  _enemyController._isHit) return;

        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_enemyController.Damage, _enemyController.gameObject);
        }
        else if(other.gameObject.TryGetComponent<Chair>(out Chair chair))
        {
            chair.Hit(_enemyController.Damage, _enemyController.gameObject);
        }
       
    }
}
