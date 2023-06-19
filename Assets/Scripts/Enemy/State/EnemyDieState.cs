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


        _enemyController._isDie = true;
        _enemyController._anim.SetTrigger("Die");

        
        
        Debug.Log("DeadState");
        
    }

    public override void Update(float deltaTime)
    {
    }
}
