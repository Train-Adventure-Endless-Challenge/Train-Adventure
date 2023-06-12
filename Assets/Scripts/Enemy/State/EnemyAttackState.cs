using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능
    
    private NavMeshAgent _agent;
    private EnemyFieldOfView _fov;

    private float _agentStopDistance;

    public override void OnEnter()
    {
        base.OnEnter();


        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;


        _agentStopDistance = _agent.stoppingDistance;
        _agent.stoppingDistance += _enemyController.AttackRange;

        _enemyController._anim.SetBool("WalkToPlayer", true);
    }

    public override void Update(float deltaTime)
    {
        if (_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _agent.SetDestination(_player.transform.position);
        }
        else if (!_enemyController._enemyFieldOfView._isVisiblePlayer && _enemyController._isCurrentAttackCor == false)
        {
            _enemyController._anim.SetBool("WalkToPlayer", false);
            _enemyController.ChangeState<EnemyIdleState>();
            return;
        }

        CheckAttack();
    }

    /// <summary>
    /// 공격 체크
    /// </summary>
    private void CheckAttack()
    {



        if (_agent.remainingDistance <= _agent.stoppingDistance && _enemyController._isCurrentAttackCor == false )
        {
            switch (_enemyController.EnemyType)
            {
                case EnemyType.range:
                    EnemyController_Range enemyR = _enemyController.GetComponent<EnemyController_Range>();
                    _enemyController.StartCoroutine(enemyR.AttackRangeCor());
                    break;
                case EnemyType.melee:
                    EnemyController_Melee enemyM = _enemyController.GetComponent<EnemyController_Melee>();
                     _enemyController.StartCoroutine(enemyM.AttackMeleeCor());
                    break;
                default:
                    break;
            }
        }
    }


    /// <summary>
    /// Animation clip에 맞춰서 실행되는 event
    /// </summary>
    public void CheckHitEvent()
    {
        // 실제 공격 체크
        if (_enemyController._enemyFieldOfView._isVisiblePlayer && Vector3.Distance(_enemyController.transform.position, _player.transform.position) < _enemyController.AttackRange + _agent.stoppingDistance)
        {
            PlayerHit player = _player.GetComponent<PlayerHit>();     //  추후 싱글톤으로 찾는다면 로직 수정
            player.Hit(_enemyController.Damage);
        }
    }


    public override void OnExit()
    {
        _enemyController._agent.stoppingDistance = _agentStopDistance;
        base.OnExit();
    }
}
