using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyHitState : State<EnemyController> 
{

    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능

    private NavMeshAgent _agent;
    public override void OnEnter()
    {
        base.OnEnter();

        if(_enemyController.EnemyType == EnemyType.Bomb)
        {
            _enemyController.ChangeState<EnemyAttackState>();
            return;
        }

        _player = PlayerManager.Instance.gameObject;

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.isStopped = true;

        // 체력 감소 및 적용
        _enemyController.Hp -= _enemyController._eventDamage;

        _enemyController._enemyUI._hpBarSlider.gameObject.SetActive(true);
        _enemyController._enemyUI.UpdateHpUI(_enemyController.Hp);

        if (_enemyController.Hp <= 0) return;

        _enemyController._anim.SetTrigger("Hit");                       // anim
    }

    public override void Update(float deltaTime)
    {
        //enemyController 기반 State 필수 구현 함수
    }
}
