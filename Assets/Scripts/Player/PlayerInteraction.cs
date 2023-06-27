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

    Outline lastDetectionObject; // 마지막으로 찾은 오브젝트
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

        if (hits.Length <= 0)
        {
            if (lastDetectionObject != null) lastDetectionObject.enabled = false;
            lastDetectionObject = null;
            return;
        }

        int i = 0; // 첫번째 오브젝트를 가져오기위한 변수


        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Interaction Object")) // 상호작용 객체라면
            {
                if (i == 0) // 첫번째라면
                {
                    // 마지막 오브젝트와 첫번째 오브젝트가 다르다면 마지막 비활성화
                    // => 움직여서 가장 가까운 오브젝트가 바뀔 경우
                    if (hit != lastDetectionObject && lastDetectionObject != null) lastDetectionObject.enabled = false;

                    lastDetectionObject = hit.GetComponentInChildren<Outline>();
                    lastDetectionObject.enabled = true;
                }
                else hit.GetComponentInChildren<Outline>().enabled = false;


                if (Input.GetKeyDown(_keyCode)) // 상호작용 키를 눌렀을 때
                {
                    hit.GetComponent<InteractionObject>().Interact(); // 상호작용 시작
                }
            }

            i++;
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
