using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyDieState : State<EnemyController>
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

        // 보스가 죽었을 때 생겨나는 효과 추가

    }

    public override void Update(float deltaTime)
    {
    }
}
