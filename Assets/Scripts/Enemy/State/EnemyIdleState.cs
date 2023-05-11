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

        _enemyController.StartCoroutine(DelayToMoveCor());
    }

    IEnumerator DelayToMoveCor()
    {
        yield return new WaitForSeconds(1f);        // 변수로도 설정 가능 추후 기획으로 정해질 시 변수 선언 

        _enemyController.ChangeState<EnemyMoveState>();
    }
    public override void Update(float deltaTime)
    {
    }

    public override void OnExit()
    {
        base.OnExit();
        _enemyController.StopAllCoroutines();
    }
}
