using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMoveState : State<EnemyController>
{
    private Transform _targetPos;       // 랜덤 이동 위치

    private float _range = 3;               // 이동 범위 (추후 enemy Data 에서 받아올 수 있음)
    private Vector3 _point;

    EnemyController _enemyController;
    NavMeshAgent _agent;
    EnemyFieldOfView _fov;

    public override void OnEnter()
    {
        _enemyController = _context.GetComponent<EnemyController>();
        _agent = _enemyController._agent;
        _fov = _enemyController._enemyFieldOfView;
        _targetPos = _enemyController._target;

        if (RandomPoint(_targetPos.position, _range, out _point))
        {
            _targetPos.position = _point;
        }

    }

    public override void Update(float deltaTime)
    {
        if (_fov.isVisiblePlayer)
        {

        }
        else
        {
            _agent.SetDestination(_targetPos.position);

            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (RandomPoint(_targetPos.position, _range, out _point))
                {
                    _targetPos.position = _point;
                }
            }
        }
    }



    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // 네비메이션 메시가 생성 안된 지역의 위치 값을 가져온 경우를 대비하여 최소 30 번 반복 (임시 수)
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
