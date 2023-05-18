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

     //private IEnumerator _currentAttackCor;
    private bool _currentAttackCor;

    public override void OnEnter()
    {
        base.OnEnter();

        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _agent.stoppingDistance += _enemyController.AttackRange;

        _enemyController._anim.SetBool("WalkToPlayer", true);
    }

    public override void Update(float deltaTime)
    {

        CheckAttack();

        if(_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _agent.SetDestination(_player.transform.position);  
        }

    }

    /// <summary>
    /// 공격 체크
    /// </summary>
    private void CheckAttack()
    {
        if(!_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _enemyController._anim.SetBool("WalkToPlayer", false);
            _enemyController.ChangeState<EnemyIdleState>();
            return;
        }

        Debug.Log(_currentAttackCor == false);

        if (_agent.remainingDistance <= _agent.stoppingDistance && _currentAttackCor == false )
        {
            switch (_enemyController.EnemyType)
            {
                case EnemyType.range:
                    _enemyController.StartCoroutine(AttackRangeCor());
                    break;
                case EnemyType.melee:
                     _enemyController.StartCoroutine(AttackMeleeCor());
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
        _currentAttackCor = true;

        EnemyController_Range enemy = _enemyController.GetComponent<EnemyController_Range>();
        enemy._laserObj.SetActive(true);

        RaycastHit hit;

        if(Physics.Raycast(enemy._laserObj.transform.position, enemy._laserObj.transform.forward, out hit, LayerMask.GetMask("Player")))
        {
            Player player =  hit.transform.gameObject.GetComponent<Player>();
            player.Hp -= _enemyController.Damage;
        }

        yield return new WaitForSeconds(_enemyController.AttackSpeed);

        enemy._laserObj.SetActive(false);

        _currentAttackCor = false;

    }

    /// <summary>
    /// 근거리 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackMeleeCor()
    {
        
        _currentAttackCor = true;

        _enemyController._agent.speed = 0;

        _enemyController._anim.SetTrigger("Attack");

        yield return new WaitForSeconds(_enemyController.AttackSpeed);

        _enemyController._agent.speed = _enemyController.MoveSpeed;

        _currentAttackCor = false;

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
