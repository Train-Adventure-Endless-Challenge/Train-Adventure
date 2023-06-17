using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackWalkState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;
    private EnemyFieldOfView _fov;

    private GameObject _player;


    public override void OnEnter()
    {
        base.OnEnter();

        _player = PlayerManager.Instance.gameObject;

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _fov = _enemyController._enemyFieldOfView;
        _enemyController._anim.SetBool("WalkToPlayer", true);


        _enemyController._isCurrentAttackCor = false;

    }

    public override void Update(float deltaTime)
    {
        if (!_fov._isVisiblePlayer)
        {
            _enemyController.ChangeState<EnemyIdleState>();
            return;
        }

        _agent.SetDestination(_player.gameObject.transform.position);

        CheckAttack();
    }

    /// <summary>
    /// 몬스터에게 접근 완료 시 && 
    /// </summary>
    void CheckAttack()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.2f && _enemyController._isCurrentAttackCor == false)
        {
            _enemyController.ChangeState<EnemyAttackState>();
        }
    }

    public override void OnExit()
    {
        _enemyController._anim.SetBool("WalkToPlayer", false);
        base.OnExit();

    }

}
