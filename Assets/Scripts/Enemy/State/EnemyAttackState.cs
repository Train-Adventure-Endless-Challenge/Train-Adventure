using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAttackState : State<EnemyController>
{
    private EnemyController _enemyController;
    private GameObject _player;         // 추후 싱글톤으로 찾기 가능
    
    private NavMeshAgent _agent;
    private EnemyFieldOfView _fov;

    private bool _isCurrentAttackCor;

    private float _agentStopDistance;

    public override void OnEnter()
    {
        base.OnEnter();


        _player = GameObject.FindGameObjectWithTag("Player");

        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;


        _agentStopDistance = _agent.stoppingDistance;
        _agent.stoppingDistance += _enemyController.AttackRange;

        _enemyController._anim.SetBool("WalkToPlayer", true);
    }

    public override void Update(float deltaTime)
    {
        if (_enemyController._enemyFieldOfView._isVisiblePlayer)
        {
            _agent.SetDestination(_player.transform.position);
        }
        else if (!_enemyController._enemyFieldOfView._isVisiblePlayer && _isCurrentAttackCor == false)
        {
            _enemyController._anim.SetBool("WalkToPlayer", false);
            _enemyController.ChangeState<EnemyIdleState>();
            return;
        }

        CheckAttack();
    }

    /// <summary>
    /// 공격 체크
    /// </summary>
    private void CheckAttack()
    {



        if (_agent.remainingDistance <= _agent.stoppingDistance && _isCurrentAttackCor == false )
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

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(_enemyController.transform.position, _player.transform.position) > _enemyController.AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            yield break;
        }

        _isCurrentAttackCor = true;

        _enemyController._agent.isStopped = true;

        float timer = 0;
        while (timer <= 0.5f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;

            Vector3 dir = _player.transform.position - _enemyController.transform.position;
            _enemyController.transform.rotation = Quaternion.Lerp(_enemyController.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            yield return new WaitForEndOfFrame();

        }

        _enemyController._anim.SetTrigger("Attack");

        EnemyController_Range enemy = _enemyController.GetComponent<EnemyController_Range>();
        enemy._laserObj.SetActive(true);

        RaycastHit hit;

        if(Physics.Raycast(enemy._laserObj.transform.position, enemy._laserObj.transform.forward, out hit, LayerMask.GetMask("Player")))
        {
            PlayerHit player =  hit.transform.gameObject.GetComponent<PlayerHit>();
            player.Hit(_enemyController.Damage);
        }

        yield return new WaitForSeconds(_enemyController.AttackSpeed);

        enemy._laserObj.SetActive(false);

        _enemyController._agent.isStopped = false;

        _isCurrentAttackCor = false;

    }

    /// <summary>
    /// 근거리 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackMeleeCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if(Vector3.Distance(_enemyController.transform.position, _player.transform.position) > _enemyController.AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            yield break;
        }

        _enemyController._agent.isStopped = true;

        float timer = 0;
        while (timer <= 0.2f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;

            Vector3 dir = _player.transform.position - _enemyController.transform.position;
            _enemyController.transform.rotation = Quaternion.Lerp(_enemyController.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            yield return new WaitForEndOfFrame();
        
        }

        _enemyController._anim.SetTrigger("Attack");

        // 실제 공격 체크
        if (_enemyController._enemyFieldOfView._isVisiblePlayer && Vector3.Distance(_enemyController.transform.position, _player.transform.position) < _enemyController.AttackRange + _agent.stoppingDistance)
        {
            PlayerHit player = _player.GetComponent<PlayerHit>();     //  추후 싱글톤으로 찾는다면 로직 수정
            player.Hit(_enemyController.Damage);
        }


        yield return new WaitForSeconds(_enemyController.AttackSpeed);

        _enemyController._agent.isStopped = false;

        _isCurrentAttackCor = false;

    }

    public override void OnExit()
    {
        _enemyController._agent.stoppingDistance = _agentStopDistance;
        base.OnExit();
    }
}
