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
        // 몬스터 타입에 따른 본인의 공격 로직 호출
        // 공격이 다 끝난 상황에서 idle state로 돌아온다 => 각 몬스터 controller에 해당 로직 존재
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
            case EnemyType.scientist:
                EnemyController_Scientist enemyS = _enemyController.GetComponent<EnemyController_Scientist>();
                enemyS.CheckAttack();
                break;
            default:
                break;
        }
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
        
        base.OnExit();

    }

}
