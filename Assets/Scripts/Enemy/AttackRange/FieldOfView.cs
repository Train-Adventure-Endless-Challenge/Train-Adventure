using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float _radius;
    [Range(0,360)]
    public float _angle;

    public GameObject _playerRef;       

    public LayerMask _targetMask;       // ��ǥ layer (ex. player)
    public LayerMask _obstructMask;     // ���ع� layer

    public bool _canSeePlayer;

    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player"); // ���� �̱��� �� �ٸ� ������� ȣ���� �� ����
        StartCoroutine(FOVCor());
    }

    private IEnumerator FOVCor()
    { 
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    /// <summary>
    /// target check
    /// </summary>
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius,_targetMask);

        if(rangeChecks.Length != 0 )
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstructMask))
                    _canSeePlayer = true;
                else 
                    _canSeePlayer = false;
            }
            else
                _canSeePlayer = false;
        }
        else if(_canSeePlayer)
            _canSeePlayer= false;
    }
}
