using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 움직임을 담당하는 클래스
/// </summary>
public class PlayerController : SceneSingleton<PlayerController>
{
    #region Variable

    #region Move

    [Header("Key")]
    [SerializeField] private KeyCode _runningKey;

    private Player _player;
    private Animator _animator;
    private CharacterController _controller;

    private Vector3 _velocity;         // 중력
    private Vector3 _moveDirection;    // 움직일 방향

    private float _speed;              // 기본 속도
    private float _moveSpeedScale;     // 현재 움직임 속도 비율
    private float _speedScale = 0.5f;  // 걷기 속도 비율
    private float _runSpeedScale = 1f; // 달리기 속도 비율
    private float _gravity = -9.81f;   // 중력 가속도

    #endregion

    #region SmoothMove

    [SerializeField] AnimationCurve _animationCurve; // 시간에 따른 변화량

    private float _lerpTime = 1f;                    // 최종 러프 시간
    private float _currentTime = 0f;                 // 현재 시간

    private IEnumerator _smoothMoveCor;              // 코루틴 변수      

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UseGravity();
    }

    private void Start()
    {
        DataInit();
    }

    #endregion

    #region Move

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 데이터 초기화 함수
    /// </summary>
    private void DataInit()
    {
        _speed = _player.Speed;
    }

    /// <summary>
    /// 플레이어의 움직임 함수
    /// <br/>
    /// Horizontal, Vertical 값으로 방향 설정
    /// 입력 값에 따른 애니메이션 변경
    /// </summary>
    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            if (Input.GetKey(_runningKey))
            {
                if (_smoothMoveCor != null)
                {
                    StopCoroutine(_smoothMoveCor);
                }
                StartCoroutine(SmoothMoveCor(_moveSpeedScale, _runSpeedScale));
            }
            else
            {
                if (_smoothMoveCor != null)
                {
                    StopCoroutine(_smoothMoveCor);
                }
                StartCoroutine(SmoothMoveCor(_moveSpeedScale, _speedScale));
            }
            _moveDirection = new Vector3(x, 0f, z);
            _controller.Move(_speed * _moveSpeedScale * Time.deltaTime * _moveDirection);
            transform.forward = _moveDirection;
            if (_player.playerState != PlayerState.Move)
            {
                _animator.SetTrigger("OnState");
                _player.playerState = PlayerState.Move;
            }
        }
        else
        {
            if (_smoothMoveCor != null)
            {
                StopCoroutine(_smoothMoveCor);
                _smoothMoveCor = null;
            }
            StartCoroutine(SmoothMoveCor(_moveSpeedScale, 0f));
            if (_moveSpeedScale <= 0f && _player.playerState != PlayerState.Idle)
            {
                _animator.SetTrigger("OnState");
                _player.playerState = PlayerState.Idle;
            }
        }
        _animator.SetFloat("MoveSpeed", Mathf.Round(_moveSpeedScale * 100) / 100); // 부동 소수점 오차 해결
    }

    /// <summary>
    /// 중력 함수
    /// <br/>
    /// 중력가속도 = 9.81 m / s * s
    /// </summary>
    private void UseGravity()
    {
        if (_controller.isGrounded == false)
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0f;
        }
        _controller.Move(Time.deltaTime * _velocity);
    }

    #endregion

    #region SmoothMove

    /// <summary>
    /// 부드러운 움직임을 위한 코루틴 함수
    /// <br/>
    /// _moveSpeedScale 값을 바꿈
    /// </summary>
    /// <param name="startValue">초기 값</param>
    /// <param name="endValue">달성 값</param>
    /// <returns></returns>
    private IEnumerator SmoothMoveCor(float startValue, float endValue)
    {
        if (_moveSpeedScale == endValue) yield break;
        _currentTime = 0f;
        _lerpTime = 1f;
        _smoothMoveCor = SmoothMoveCor(startValue, endValue);
        while (_moveSpeedScale != endValue)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _lerpTime)
            {
                _currentTime = _lerpTime;
            }
            float curveValue = _animationCurve.Evaluate(_currentTime / _lerpTime);

            _moveSpeedScale = Mathf.Lerp(startValue, endValue, curveValue);

            yield return new WaitForEndOfFrame();
        }
        _smoothMoveCor = null;
    }

    #endregion

    #endregion
}
