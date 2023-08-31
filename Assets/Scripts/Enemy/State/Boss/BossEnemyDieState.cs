using UnityEngine;
using UnityEngine.AI;

public class BossEnemyDieState : State<EnemyController>
{
    private EnemyController _enemyController;
    private NavMeshAgent _agent;

    private readonly int _dieId = Animator.StringToHash("Die");

    public override void OnEnter()
    {
        base.OnEnter();

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        _enemyController._isDie = true;
        _enemyController._anim.SetTrigger(_dieId);

        // 보스가 죽었을 때 생겨나는 효과 추가

        SoundManager.Instance.TrainBgSoundPlay(); // 기차 배경음악 재생
    }

    public override void Update(float deltaTime)
    {
    }
}
