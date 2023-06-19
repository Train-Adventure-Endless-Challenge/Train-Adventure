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

        //if (_enemyController._isDie) return;
        
        _enemyController._anim.SetTrigger("Die");
        _enemyController._isDie = true;

        
    }

    public override void Update(float deltaTime)
    {
        Debug.Log("DeadState");
    }
}
