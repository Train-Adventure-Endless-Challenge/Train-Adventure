using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyIdleState : State<EnemyController>
{

    EnemyController _enemyController;


    public override void OnEnter()
    {
        base.OnEnter();


        _enemyController = _context.GetComponent<EnemyController>();

        _enemyController._anim.SetBool("Walk", false);

        _enemyController._agent.isStopped = true;

        _enemyController.StartCoroutine(StartAttackWalkCor());
    }

    IEnumerator StartAttackWalkCor()
    {
        yield return new WaitForSeconds(3f);        // 3초 마다 공격
         _enemyController.ChangeState<BossEnemyAttackWalkState>(); 
    }

    public override void Update(float deltaTime)
    {
        if(!_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _enemyController.ChangeState<BossEnemyAttackWalkState>();
        }
    }

    public override void OnExit()
    {
        _enemyController._agent.isStopped = false;
        _enemyController.StopAllCoroutines();

        base.OnExit();
    }
}
