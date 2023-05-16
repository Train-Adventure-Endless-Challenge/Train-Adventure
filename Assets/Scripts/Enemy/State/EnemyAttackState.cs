using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능
    
    private NavMeshAgent _agent;
    private EnemyFieldOfView _fov;

    private Coroutine _currentAttackCor;

    public override void OnEnter()
    {
        base.OnEnter();

        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.stoppingDistance += _enemyController.AttackRange;        
       
    }

    public override void Update(float deltaTime)
    {
        _agent.SetDestination(_player.transform.position);

        CheckAttack();
    }

    /// <summary>
    /// 공격 체크
    /// </summary>
    private void CheckAttack()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && _currentAttackCor == null )
        {
            switch (_enemyController.EnemyType)
            {
                case EnemyType.range:
                    _currentAttackCor = _enemyController.StartCoroutine(AttackRangeCor());
                    break;
                case EnemyType.melee:
                    _currentAttackCor = _enemyController.StartCoroutine(AttackMeleeCor());
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 원거리 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackRangeCor()
    {
        EnemyController_Range enemy = _enemyController.GetComponent<EnemyController_Range>();
        enemy._laserObj.SetActive(true);

        RaycastHit hit;

        if(Physics.Raycast(enemy._laserObj.transform.position, enemy._laserObj.transform.forward, out hit, LayerMask.GetMask("Player")))
        {
            Player player =  hit.transform.gameObject.GetComponent<Player>();
            player.Hp -= _enemyController.damage;
        }

        yield return new WaitForSeconds(_enemyController.AttackSpeed);

        enemy._laserObj.SetActive(false);

        _currentAttackCor = null;

    }

    /// <summary>
    /// 근거리 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackMeleeCor()
    {
        yield return null;
    }
}
