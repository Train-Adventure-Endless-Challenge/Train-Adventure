using UnityEngine;

/// <summary>
/// 플레이어 상호작용을 담당하는 클래스
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] private float _detectionAngle = 90f; // 감지할 부채꼴의 각도
    [SerializeField] private float _range = 1f;
    [SerializeField] private Color _color = Color.green;

    [Header("Layer")]
    [SerializeField] private LayerMask _targetLayer;      // 감지할 대상의 레이어

    [SerializeField] private KeyCode _keyCode;

    public void Interact()
    {
        Vector3 origin = transform.position;
        Collider[] hits;

        hits = Physics.OverlapSphere(origin, _range, _targetLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Interaction Object"))
            {
                // 적의 위치를 가져와서 플레이어와의 각도를 계산
                Vector3 direction = hit.transform.position - origin;
                float angle = Vector3.Angle(transform.forward, direction);

                // 감지 범위 내에 있는지 체크
                if (angle < _detectionAngle * 0.5f)
                {
                    if (Input.GetKeyDown(_keyCode))
                    {
                        hit.GetComponent<InteractionObject>().Interact();
                        print(1);
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
}
