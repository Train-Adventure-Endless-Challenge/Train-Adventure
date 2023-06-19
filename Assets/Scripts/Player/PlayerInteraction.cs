using UnityEngine;

/// <summary>
/// 플레이어 상호작용을 담당하는 클래스
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private Transform _playerTransform;

    [Header("Layer")]
    [SerializeField] private LayerMask _targetLayer; // 감지할 대상의 레이어

    [Header("Key")]
    [SerializeField] private KeyCode _keyCode; // 상호작용 키

    private float _detectionAngle; // 감지할 부채꼴의 각도
    private float _range;          // 감지 범위
    private Color _color;          // 부채꼴 색상

    private Player _player;

    #endregion

    #region Function

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        DataInit();
    }

    private void Init()
    {
        _player = _playerTransform.GetComponent<Player>();
    }

    /// <summary>
    /// 데이터 초기화를 담당하는 함수
    /// </summary>
    private void DataInit()
    {
        _detectionAngle = _player.InteractionAngle;
        _range = _player.InteractionRange;
        _color = _player.InteractionColor;
    }

    /// <summary>
    /// 상호작용 함수
    /// <br/> 상호작용 객체가 감지, 입력 처리
    /// </summary>
    public void Interact()
    {
        Vector3 origin = transform.position; // 위치 설정
        Collider[] hits;

        hits = Physics.OverlapSphere(origin, _range, _targetLayer); // 객체 감지

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Interaction Object")) // 상호작용 객체라면
            {
                // 적의 위치를 가져와서 플레이어와의 각도를 계산
                Vector3 direction = hit.transform.position - origin;
                float angle = Vector3.Angle(transform.forward, direction);

                // 감지 범위 내에 있는지 체크
                if (angle < _detectionAngle * 0.5f)
                {
                    if (Input.GetKeyDown(_keyCode)) // 상호작용 키를 눌렀을 때
                    {
                        hit.GetComponent<InteractionObject>().Interact(); // 상호작용 시작
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Scene 뷰에서 감지 영역을 시각적으로 표시
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _range);

        Vector3 leftBoundary = Quaternion.Euler(0, -_detectionAngle * 0.5f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, _detectionAngle * 0.5f, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * _range);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * _range);
    }

    #endregion
}
