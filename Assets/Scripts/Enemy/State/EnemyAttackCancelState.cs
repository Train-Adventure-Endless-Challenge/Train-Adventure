using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.AI;

public class EnemyAttackCancelState : State<EnemyController>
{
    EnemyController _enemyController;
    private NavMeshAgent _agent;

    private float _timer = 0f;

    public override void OnEnter()
    {
        base.OnEnter();

        _timer = 0;

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        _enemyController._anim.SetTrigger("AttackCancel");
        switch (_enemyController.EnemyType)
        {
            case EnemyType.range:
                break;
            case EnemyType.melee:
                (_enemyController as EnemyController_Melee).CheckHitEndEffect();
                break;
            case EnemyType.scientist:
                break;
            case EnemyType.Bomb:
                break;
            case EnemyType.Boss:
                break;
            default:
                break;
        }
    }

    public override void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _enemyController._anim.GetCurrentAnimatorClipInfo(0).Length + 0.7f)
        {
            _enemyController.ChangeState<EnemyIdleState>();
        }
    }

    public override void OnExit()
    {
        _agent.isStopped = false;

        base.OnExit();
    }
}
