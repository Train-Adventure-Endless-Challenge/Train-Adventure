// 작성자 : 박재만
// 작성일 : 2023-06-18

#region Namespace

using UnityEngine;
using System.Collections;

#endregion

/// <summary>
/// 플레이어의 움직임을 담당하는 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Variable

    #region Move

    [Header("Key")]
    [SerializeField] private KeyCode _runningKey;

    [Header("Variable")]
    public float _moveSpeedScale; // 현재 움직임 속도 비율

    #region Class

    private Player _player;
    private Animator _animator;
    private CharacterController _controller;

    #endregion

    private Vector3 _velocity;      // 중력 방향
    private Vector3 _moveDirection; // 움직일 방향

    private float _speed;            // 기본 속도
    private float _gravity = -9.81f; // 중력 가속도
    private float _walkSpeedScale;   // 걷기 속도 비율
    private float _runSpeedScale;    // 달리기 속도 비율
    private float _slowSpeedScale;   // 감속 비율 

    #endregion

    #region SmoothMove

    [SerializeField] AnimationCurve _animationCurve; // 시간에 따른 변화량

    private float _lerpTime = 1f;    // 최종 러프 시간
    private float _currentTime = 0f; // 현재 시간

    private Coroutine _smoothMoveCor;      // 부드러운 움직임 코루틴 변수      
    private Coroutine _changeSlowSpeedCor; // 속도 변화 코루틴 변수

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    void Awake()
    {
        Init(); // 초기화 진행
    }

    // Update is called once per frame
    void Update()
    {
        UseGravity(); // 중력
    }

    private void Start()
    {
        DataInit(); // 데이터 초기화
    }

    #endregion

    #region Move

    /// <summary>
    /// 초기화를 담당하는 함수
    /// <br/>
    /// 플레이어, 애니메이션 등 초기화
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// 데이터 초기화 함수
    /// <br/>
    /// 데이터 값에서 플레이어의 속도를 초기화
    /// </summary>
    private void DataInit()
    {
        _speed = _player.Speed;
        _walkSpeedScale = _player.WalkSpeedScale;
        _runSpeedScale = _player.RunSpeedScale;
        _slowSpeedScale = _player.MoveSlowSpeedScale;
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
        if (x != 0 || z != 0) // 움직이고 있다면
        {
            if (Input.GetKey(_runningKey)) // 달리기 키를 눌렀을 때
            {
                _smoothMoveCor = StartCoroutine(SmoothMoveCor(_moveSpeedScale, _runSpeedScale)); // 달리기 코루틴 실행
            }
            else
            {
                _smoothMoveCor = StartCoroutine(SmoothMoveCor(_moveSpeedScale, _walkSpeedScale)); // 걷기 코루틴 실행
            }
            if (_player.playerState != PlayerState.Attack)
            {
                _moveDirection = new Vector3(x, 0f, z).normalized; // 움직임 방향
            }
            _controller.Move(_speed * _slowSpeedScale * _moveSpeedScale * Time.deltaTime * _moveDirection); // 움직이기
            transform.forward = _moveDirection;                                                             // 움직이는 방향으로 플레이어 시점 초기화
            if (_player.playerState != PlayerState.Move && _player.playerState != PlayerState.Attack)       // 상태를 초기화 해아할 때
            {
                _animator.SetTrigger("OnState");                                                            // 상태 변경 트리거
                _player.playerState = PlayerState.Move;                                                     // 상태 초기화
            }
        }
        else
        {
            _smoothMoveCor = StartCoroutine(SmoothMoveCor(_moveSpeedScale, 0f));                                               // 멈춤 코루틴 실행
            if (_moveSpeedScale <= 0f && _player.playerState != PlayerState.Idle && _player.playerState != PlayerState.Attack) // 상태를 초기화 해야할 때
            {
                _animator.SetTrigger("OnState");                                                                               // 상태 변경 트리거
                _player.playerState = PlayerState.Idle;                                                                        // 상태 변경
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
        if (_controller.isGrounded == false)          // 땅에 닿아 있지 않다면
        {
            _velocity.y += _gravity * Time.deltaTime; // 중력 방향 할당
        }
        else
        {
            _velocity.y = 0f;                         // 방향 초기화
            return;
        }
        _controller.Move(Time.deltaTime * _velocity); // 중력 적용
    }

    /// <summary>
    /// 실행되고 있는 움직임 코루틴을 종료하는 함수
    /// </summary>
    public void StopMove()
    {
        if (_smoothMoveCor != null)        // 코루틴이 실행 중이라면
        {
            StopCoroutine(_smoothMoveCor); // 코루틴 종료
            _smoothMoveCor = null;         // 코루틴 초기화
        }
    }

    /// <summary>
    /// 공격시 속도 변화를 위한 함수
    /// </summary>
    /// <param name="endValue">달성 값</param>
    /// <param name="lerpTime">러프 시간</param>
    public void ChangeSlowSpeed(float endValue, float lerpTime)
    {
        if (_changeSlowSpeedCor != null)                        // 코루틴이 실행 중이라면
        {
            StopCoroutine(_changeSlowSpeedCor);                 // 코루틴 종료
            _changeSlowSpeedCor = null;                         // 코루틴 초기화
        }
        _changeSlowSpeedCor = StartCoroutine(ChangeSlowSpeedCor(endValue, lerpTime)); // 감속 코루틴 실행
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
        if (_moveSpeedScale == endValue)                                           // 이미 달성 값에 도달 했다면
        {
            yield break;                                                           // 코루틴 종료
        }
        _lerpTime = 1f;                                                            // 러프 시간 
        _currentTime = 0f;                                                         // 현재 시간
        while (_moveSpeedScale != endValue)                                        // 달성값과 일치 할 때까지 진행
        {
            _currentTime += Time.deltaTime;                                        // 현재 시간 더하기

            if (_currentTime >= _lerpTime)                                         // 러프 시간 보다 크다면
            {
                _currentTime = _lerpTime;                                          // 값 일치 시키기
            }
            float curveValue = _animationCurve.Evaluate(_currentTime / _lerpTime); // 애니메이션 커브 x값을 시간과 일치 시켜줌

            _moveSpeedScale = Mathf.Lerp(startValue, endValue, curveValue);        // 움직임의 값을 시간 만큼 Lerp 시킴

            yield return new WaitForEndOfFrame();
        }
        _smoothMoveCor = null;                                                     // 코루틴 초기화
    }

    /// <summary>
    /// 공격시 속도 변화를 위한 코루틴 함수
    /// <br/> _slowSpeedScale 값을 바꿈
    /// </summary>
    /// <param name="endValue">달성 값</param>
    /// <param name="lerpTime">러프 시간</param>
    /// <returns></returns>
    private IEnumerator ChangeSlowSpeedCor(float endValue, float lerpTime)
    {
        if (_slowSpeedScale == endValue)                                         // 이미 최종 값에 도달 했다면
        {
            yield break;                                                         // 코루틴 종료
        }
        float currentTime = 0f;                                                  // 현재 시간
        float startValue = _slowSpeedScale;                                      // 시작 값
        while (_slowSpeedScale != endValue)                                      // 달성값과 일치 할 때 까지 진행
        {
            currentTime += Time.deltaTime;                                       // 현재 시간 더하기

            if (currentTime >= lerpTime)                                         // 러프 시간 보다 크다면
            {
                currentTime = lerpTime;                                          // 값 일치 시키기
            }
            float curveValue = _animationCurve.Evaluate(currentTime / lerpTime); // 애니메이션의 커브 x값을 시간과 일치 시켜줌

            _slowSpeedScale = Mathf.Lerp(startValue, endValue, curveValue);      // 감속의 값을 시간 만큼 Lerp 시킴

            yield return new WaitForEndOfFrame();
        }
        _changeSlowSpeedCor = null;                                              // 코루틴 초기화
    }

    #endregion

    #endregion
}
