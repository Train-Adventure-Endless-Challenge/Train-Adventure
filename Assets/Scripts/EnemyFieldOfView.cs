using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public float _radius;               // 탐색범위
    [Range(0, 360)]
    public float _angle;                // 시야각
    public float _viewCheckDelay = 0.2f;// 적 탐색 딜레이

    public GameObject _playerRef;       // 플레이어     

    public LayerMask _targetMask;       // 목표 layer (ex. player)
    public LayerMask _obstructMask;     // 방해물 layer

    public bool _isVisiblePlayer;          //목표 (플레이어)가 범위 내에 있는가

    private void Start()
    {
        _playerRef = PlayerManager.Instance.gameObject; 
        // 추후 EnemyRange 함수에 _radius 적용
        
        StartCoroutine(FOVCor());
    }

    private IEnumerator FOVCor()
    {
        WaitForSeconds wait = new WaitForSeconds(_viewCheckDelay);

        while (true)
        {

            yield return wait;
            CheckFieldOfView();
        }
    }

    /// <summary>
    /// 타겟(플레이어)가 범위 안에 있는지 확인
    /// </summary>
    private void CheckFieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructMask))
                    _isVisiblePlayer = true;
                else
                    _isVisiblePlayer = false;
            }
            else
                _isVisiblePlayer = false;
        }
        else if (_isVisiblePlayer)
            _isVisiblePlayer = false;
    }
}
