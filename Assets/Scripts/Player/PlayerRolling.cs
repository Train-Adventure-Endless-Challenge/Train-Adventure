using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 구르기를 담당하는 클래스
/// </summary>
public class PlayerRolling : MonoBehaviour
{
    #region Variable

    [Header("Key")]
    [SerializeField] private KeyCode _rollingKey;

    [Header("Attribute")]
    [SerializeField] private float _rollingRange;
    [SerializeField] private AnimationCurve _rollingCurve;
    [SerializeField] private int _staminaValue = 20;       // 필요 스태미너 값

    [SerializeField] private StaminaSlider _staminaSlider;

    public bool _isGodMode;

    private Player _player;
    private Animator _animator;
    private CharacterController _characterController;

    private float _lerpTime = 1f;
    private float _currentTime = 0f;

    private IEnumerator _rollCor;

    private Vector3 _endPosition;
    private Vector3 _moveDirection;

    #endregion

    #region Function

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 구르기 함수
    /// </summary>
    #region Mobile
    ///// <param name="isPC">플랫폼 테스트용 변수 ※추후 삭제※</param>
    //public void Roll(bool isPC)
    #endregion
    public void Roll()
    {
        if (_rollCor == null)
        {
            #region Mobile

            //if ((isPC && Input.GetKeyDown(_rollingKey)) || !isPC)

            #endregion
            if (Input.GetKeyDown(_rollingKey) && CanRoll())
            {
                _player.Stamina -= _staminaValue;
                IngameUIController.Instance.UpdateStemina(_player.Stamina, _player._maxStamina);
                _player.playerState = PlayerState.Rolling;
                _rollCor = RollCor(transform.position);
                StartCoroutine(RollCor(transform.position));
            }
        }
    }

    /// <summary>
    /// 구르기가 가능한지 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool CanRoll()
    {
        return _player.Stamina - _staminaValue >= 0; // 스태미나가 충분한지 체크
    }

    /// <summary>
    /// 구르기 코루틴 함수
    /// </summary>
    /// <param name="startPosition">초기 값</param>
    /// <returns></returns>
    public IEnumerator RollCor(Vector3 startPosition)
    {
        _currentTime = 0f;
        _lerpTime = 0.65f;
        _isGodMode = true;
        _animator.SetBool("IsRoll", true);
        _animator.SetTrigger("OnState");
        _endPosition = transform.forward * _rollingRange;
        while (_currentTime != _lerpTime)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _lerpTime)
            {
                _currentTime = _lerpTime;
            }

            float _curveValue = _rollingCurve.Evaluate(_currentTime / _lerpTime);

            _moveDirection = Vector3.Lerp(Vector3.zero, _endPosition, _curveValue);

            _characterController.Move(Time.deltaTime * _moveDirection);

            transform.forward = _moveDirection;

            yield return new WaitForEndOfFrame();
        }
        _isGodMode = false;
        _animator.SetBool("IsRoll", false);
        _animator.SetTrigger("OnState");
        _rollCor = null;
        _player.playerState = PlayerState.Idle;
    }

    #endregion
}
