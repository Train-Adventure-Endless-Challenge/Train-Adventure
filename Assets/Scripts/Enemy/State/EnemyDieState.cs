using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDieState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;

    public override void OnEnter()
    {
        base.OnEnter();
        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;

        _agent.isStopped = true;
        _enemyController.StartCoroutine(DieCor());
        
    }

    IEnumerator DieCor()
    {
        _enemyController._anim.SetTrigger("Die");
        yield return new WaitForSeconds(5f);

        _enemyController.DestroyGameObject();       // 추후 이쁘게? 사라지는 모션 추가 가능

    }

    public override void Update(float deltaTime)
    {
        Debug.Log("DeadState");
    }
}
