using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State<EnemyController>
{
    EnemyController _enemyController;


    public override void OnEnter()
    {
        base.OnEnter();


        _enemyController = _context.GetComponent<EnemyController>();

        if(_enemyController.HP <= 0)
        {
            _enemyController.ChangeState<EnemyDieState>();
        }

        _enemyController._anim.SetBool("Walk",false);
        _enemyController._anim.SetBool("WalkToPlayer", false);


        _enemyController.StartCoroutine(DelayToMoveCor());
    }

    IEnumerator DelayToMoveCor()
    {
        _enemyController._agent.isStopped= true;
        yield return new WaitForSeconds(1f);        // 변수로도 설정 가능 추후 기획으로 정해질 시 변수 선언 

        _enemyController.ChangeState<EnemyMoveState>();
    }
    public override void Update(float deltaTime)
    {
        if (_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _enemyController.ChangeState<EnemyAttackWalkState>();
        }
    }

    public override void OnExit()
    {
        _enemyController._agent.isStopped = false;
        _enemyController.StopAllCoroutines();

        base.OnExit();
    }
}
