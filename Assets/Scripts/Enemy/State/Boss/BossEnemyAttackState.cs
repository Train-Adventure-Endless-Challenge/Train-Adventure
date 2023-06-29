using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;


    public override void OnEnter()
    {
        base.OnEnter();

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        if (!_enemyController._isCurrentAttackCor)
            Attack();
        else
            _enemyController.ChangeState<BossEnemyIdleState>();
    }

    private void Attack()
    {
        _enemyController.Attack();
    }

    public override void Update(float deltaTime)
    {
        // 플레이어가 보이지 않고 현재 공격이 실행중이 아닐때
        if (!_enemyController._enemyFieldOfView._isVisiblePlayer && _enemyController._isCurrentAttackCor == false)
        {
            _enemyController.ChangeState<BossEnemyIdleState>();
        }

    }



    public override void OnExit()
    {
        _enemyController.StopAllCoroutines();
        _enemyController._agent.isStopped = false;
        base.OnExit();

    }
}
