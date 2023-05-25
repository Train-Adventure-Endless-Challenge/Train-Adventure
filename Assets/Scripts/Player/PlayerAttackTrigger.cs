using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 플레이어의 공격 판정을 담당하는 클래스
/// </summary>
public class PlayerAttackTrigger : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    public float _detectionAngle = 90f; // 감지할 부채꼴의 각도
    public float _detectionRadius = 1f; // 감지할 부채꼴의 반지름

    [Header("Layer")]
    public LayerMask _targetLayer;      // 감지할 대상의 레이어

    private List<Collider> _detectionLists = new List<Collider>();

    private Player _player;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        // 플레이어의 위치를 기준으로 감지 영역을 생성
        Vector3 origin = transform.position;
        Collider[] hits = Physics.OverlapSphere(origin, _detectionRadius, _targetLayer);

        foreach (Collider hit in hits)
        {
            // 적의 위치를 가져와서 플레이어와의 각도를 계산
            Vector3 direction = hit.transform.position - origin;
            float angle = Vector3.Angle(transform.forward, direction);

            // 감지 범위 내에 있는지, 중복이 아닌지 체크
            if (angle < _detectionAngle * 0.5f && _detectionLists.Contains(hit) == false)
            {
                // 감지한 적에 대한 처리를 수행
                _detectionLists.Add(hit);
                hit.GetComponent<EnemyController>().Hit((int)_player.Damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Scene 뷰에서 감지 영역을 시각적으로 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -_detectionAngle * 0.5f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _detectionAngle * 0.5f, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * _detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * _detectionRadius);
    }

    private void OnEnable()
    {
        _detectionLists.Clear(); // List 초기화
    }

    #endregion

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
    }

    #endregion
}
