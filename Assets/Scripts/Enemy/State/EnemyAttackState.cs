using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능
    
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

    public override void Update(float deltaTime)    
    {

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
