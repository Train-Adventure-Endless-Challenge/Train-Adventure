using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State<EnemyController>
{
    EnemyController _enemyController;


    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("fdf");

        _enemyController = _context.GetComponent<EnemyController>();
        _enemyController.StartCoroutine(DelayToMoveCor());
    }

    IEnumerator DelayToMoveCor()
    {
        _enemyController._agent.speed = 0;
        yield return new WaitForSeconds(1f);        // 변수로도 설정 가능 추후 기획으로 정해질 시 변수 선언 

        _enemyController.ChangeState<EnemyMoveState>();
    }
    public override void Update(float deltaTime)
    {

    }

    public override void OnExit()
    {
        _enemyController.StopAllCoroutines();
        _enemyController._agent.speed = _enemyController.AttackSpeed;
        base.OnExit();
    }
}
