using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : Weapon
{
    [Header("Variable")]
    [SerializeField] private float _detectionAngle = 90f; // 감지할 부채꼴의 각도

    [Header("Layer")]
    public LayerMask _targetLayer;      // 감지할 대상의 레이어

    [SerializeField] private TrailRenderer _trailRenderer;

    //private List<Collider> _detectionLists = new List<Collider>();
    private List<RaycastHit> _detectionLists = new List<RaycastHit>();

    protected override void Init()
    {
        base.Init();
    }

    public override void Attack()
    {
        base.Attack();
        _detectionLists.Clear();
        _trailRenderer.enabled = true;
        StartCoroutine(AttackCor());
    }

    private IEnumerator AttackCor()
    {
        float currentTime = 0;

        // 플레이어의 위치를 기준으로 감지 영역을 생성
        Vector3 origin = _playerTransform.position;
        //Collider[] hits;
        RaycastHit[] hits;

        while (AttackSpeed > currentTime)
        {
            currentTime += Time.deltaTime;

            //hits = Physics.OverlapSphere(origin, _range, _targetLayer);

            hits = Physics.SphereCastAll(origin, _range, Vector3.forward, 1 << 9);

            foreach (RaycastHit hit in hits)
            {
                print(1);
                // 적의 위치를 가져와서 플레이어와의 각도를 계산
                Vector3 direction = hit.transform.position - origin;
                float angle = Vector3.Angle(transform.forward, direction);

                // 감지 범위 내에 있는지, 중복이 아닌지 체크
                if (angle < _detectionAngle * 0.5f && _detectionLists.Contains(hit) == false)
                {
                    // 감지한 적에 대한 처리를 수행
                    _detectionLists.Add(hit);
                    hit.collider.GetComponent<EnemyController>().Hit(_damage);
                }
            }
            yield return new WaitForEndOfFrame();
        }

        _trailRenderer.enabled = false;
        yield return null;
    }

    private void OnDrawGizmos()
    {
        if (_playerTransform != null)
        {
            // Scene 뷰에서 감지 영역을 시각적으로 표시
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_playerTransform.position, _range);

            Vector3 leftBoundary = Quaternion.Euler(0, -_detectionAngle * 0.5f, 0) * _playerTransform.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, _detectionAngle * 0.5f, 0) * _playerTransform.forward;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_playerTransform.position, _playerTransform.position + leftBoundary * _range);
            Gizmos.DrawLine(_playerTransform.position, _playerTransform.position + rightBoundary * _range);
        }
    }
}
