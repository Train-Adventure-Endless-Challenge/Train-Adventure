using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.AI;

public class EnemyDiscoveryState : State<EnemyController>
{
    EnemyController _enemyController;
    private NavMeshAgent _agent;

    private float timer = 0f;

    public override void OnEnter()
    {
        base.OnEnter();

        timer = 0;

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        _enemyController._anim.SetTrigger("Discovery");

        _enemyController._enemyUI.ExclamationMarkToggle(true);
    }

    public override void Update(float deltaTime)
    {                       
        timer += deltaTime;

        if (timer > 1f)
        {
            if (_enemyController._enemyFieldOfView._isVisiblePlayer)
            {
                _enemyController.ChangeState<EnemyAttackWalkState>();
            }
            else
            {
                _enemyController.ChangeState<EnemyIdleState>();
            }
        }
    }

    public override void OnExit()
    {
        _agent.isStopped = false;

        base.OnExit();
    }
}
