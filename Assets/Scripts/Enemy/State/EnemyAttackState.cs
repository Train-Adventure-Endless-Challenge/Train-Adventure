using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;     
    
    private NavMeshAgent _agent;


    public override void OnEnter()
    {
        base.OnEnter();

        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        Attack();
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
            _enemyController.ChangeState<EnemyIdleState>();
        }

    }



    public override void OnExit()
    {
        _enemyController.StopAllCoroutines();
        _enemyController._agent.isStopped = false;
        base.OnExit();

    }

}
