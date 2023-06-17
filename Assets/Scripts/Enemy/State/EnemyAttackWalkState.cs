using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackWalkState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;
    private EnemyFieldOfView _fov;

    private PlayerManager _player => PlayerManager.Instance;

    private float _agentStopDistance;


    public override void OnEnter()
    {
        base.OnEnter();

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _fov = _enemyController._enemyFieldOfView;

        _agent.isStopped = false;
        _agentStopDistance = _agent.stoppingDistance;
        _agent.stoppingDistance += _enemyController.AttackRange;
        _enemyController._anim.SetBool("WalkToPlayer", true);


    }

    public override void Update(float deltaTime)
    {
        if (!_fov._isVisiblePlayer)
        {
            _enemyController.ChangeState<EnemyIdleState>();
        }

        _agent.SetDestination(_player.transform.position);

        CheckAttack();
    }

    /// <summary>
    /// 
    /// </summary>
    void CheckAttack()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && _enemyController._isCurrentAttackCor == false)
        {
            _enemyController.ChangeState<EnemyAttackState>();
        }
    }

    public override void OnExit()
    {
        _enemyController._agent.stoppingDistance = _agentStopDistance;
        base.OnExit();
    }

}
