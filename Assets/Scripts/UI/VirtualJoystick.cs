using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 모바일 버전의 조이스틱을 담당하는 클래스
/// </summary>
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Variable

    [SerializeField] private RectTransform _lever;
    [SerializeField] private float _leverRange;

    private PlayerManager _playerManager;
    private RectTransform _rectTransform;

    private Vector2 _inputDirection;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        StartCoroutine(InputCor());
    }

    #endregion

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _lever.anchoredPosition = Vector2.zero;
        _inputDirection = Vector2.zero;
    }

    /// <summary>
    /// 레버의 움직임을 조종하는 함수
    /// </summary>
    /// <param name="eventData"></param>
    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - _rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < _leverRange ? inputPos : inputPos.normalized * _leverRange;
        _lever.anchoredPosition = inputVector;
        _inputDirection = inputVector / _leverRange;
    }

    /// <summary>
    /// 입력값에 대한 업데이트 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator InputCor()
    {
        while (true)
        {
            //_playerManager._inputDirection = _inputDirection;
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}
