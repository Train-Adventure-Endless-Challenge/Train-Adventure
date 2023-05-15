using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _lever;
    [SerializeField] private float _leverRange;

    private PlayerManager _playerManager;
    private RectTransform _rectTransform;

    private Vector2 _inputDirection;
   
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        StartCoroutine(InputCor());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    // 오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    // 하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _lever.anchoredPosition = Vector2.zero;
        _inputDirection = Vector2.zero;
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - _rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < _leverRange ? inputPos : inputPos.normalized * _leverRange;
        _lever.anchoredPosition = inputVector;
        _inputDirection = inputVector / _leverRange;
    }

    private IEnumerator InputCor()
    {
        while (true)
        {
            _playerManager.inputDirection = _inputDirection;
            yield return new WaitForEndOfFrame();
        }
    }
}
