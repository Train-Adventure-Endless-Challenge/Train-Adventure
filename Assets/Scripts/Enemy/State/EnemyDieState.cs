using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDieState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;

    bool isDieCor = false;

    public override void OnEnter()
    {
        
        base.OnEnter();


        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;

        _enemyController._isDie = true;


        _agent.isStopped = true;

        if(!isDieCor)
            _enemyController.StartCoroutine(DieCor());
        
    }

    IEnumerator DieCor()
    {
        isDieCor= true;
        _enemyController._anim.SetTrigger("Die");
        yield return new WaitForSeconds(5f);

        _enemyController.Die();       // 추후 이쁘게? 사라지는 모션 추가 가능

    }

    public override void Update(float deltaTime)
    {
        Debug.Log("DeadState");
    }
}
