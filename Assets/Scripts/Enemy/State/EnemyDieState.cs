using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDieState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;

    public override void OnEnter()
    {
        
        base.OnEnter();


        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        // enemy trail 비활성화
        if(_enemyController.TryGetComponent<EnemyController_Melee>(out EnemyController_Melee enemy))    
        {
            enemy._attackTrail.gameObject.SetActive(false);
        }

        _enemyController._isDie = true;
        _enemyController._anim.SetTrigger("Die");
        
    }

    public override void Update(float deltaTime)
    {
    }
}
