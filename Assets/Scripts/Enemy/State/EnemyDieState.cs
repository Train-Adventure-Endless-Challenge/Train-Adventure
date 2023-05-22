using System.Collections;
using System.Collections.Generic;
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

        _enemyController.StartCoroutine(DieCor());
        
    }

    IEnumerator DieCor()
    {
        _enemyController._anim.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        _enemyController.DestroyGameObject();

    }

    public override void Update(float deltaTime)
    {
        Debug.Log("DeadState");
    }
}
