using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyAttackWalkState : State<EnemyController>
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

        // 초기화
        _agent = _enemyController._agent;
        _fov = _enemyController._enemyFieldOfView;
        _enemyController._anim.SetBool("Attack", true);

        _enemyController._isCurrentAttackCor = false;

    }

    public override void Update(float deltaTime)
    {
        if (_enemyController._isDie) return;

        if (!_fov._isVisiblePlayer)     // 시야에 플레이어가 보이지 않는다면 
        {
            _enemyController.ChangeState<BossEnemyIdleState>();
            return;
        }

        _agent.SetDestination(_player.gameObject.transform.position);

        CheckAttack();
    }

    /// <summary>
    /// 몬스터에게 접근 완료 시 && 공격을 실행중이 아닐때
    /// 0.2f는 navmesh로 거리를 체크할때 약간의 오차를 방지하기 위함의 임시 수
    /// </summary>
    void CheckAttack()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.2f && _enemyController._isCurrentAttackCor == false)
        {
            //_enemyController.ChangeState<EnemyAttackState>();
        }
    }

    public override void OnExit()
    {
        _enemyController._anim.SetBool("Attack", false);
        base.OnExit();

    }
}
