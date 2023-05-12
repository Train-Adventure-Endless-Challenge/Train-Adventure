using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능

    public override void OnEnter()
    {
        base.OnEnter();

        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _enemyController._agent.stoppingDistance += _enemyController.AttackRange;        
       
    }

    public override void Update(float deltaTime)
    {
        _enemyController._agent.SetDestination(_player.transform.position);
        Debug.Log("공격");
    }
}
