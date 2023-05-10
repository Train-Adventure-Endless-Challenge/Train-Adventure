using System.Collections;
using UnityEngine;

/// <summary>
/// �÷��̾��� �������� ����ϴ� Ŭ����
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

    private Vector3 _velocity;         // �߷�
    private Vector3 _moveDirection;    // ������ ����

    private float _speed;              // �⺻ �ӵ�
    private float _moveSpeedScale;     // ���� ������ �ӵ� ����
    private float _speedScale = 0.5f;  // �ȱ� �ӵ� ����
    private float _runSpeedScale = 1f; // �޸��� �ӵ� ����
    private float _gravity = -9.81f;   // �߷� ���ӵ�

    #endregion

    #region SmoothMove

    [SerializeField] AnimationCurve _animationCurve; // �ð��� ���� ��ȭ��

    private float _lerpTime = 1f;                    // ���� ���� �ð�
    private float _currentTime = 0f;                 // ���� �ð�

    private IEnumerator _smoothMoveCor;              // �ڷ�ƾ ����      

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
    /// �ʱ�ȭ �Լ�
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// ������ �ʱ�ȭ �Լ�
    /// </summary>
    private void DataInit()
    {
        _speed = _player.Speed;
    }

    /// <summary>
    /// �÷��̾��� ������ �Լ�
    /// <br/>
    /// Horizontal, Vertical ������ ���� ����
    /// �Է� ���� ���� �ִϸ��̼� ����
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
        _animator.SetFloat("MoveSpeed", Mathf.Round(_moveSpeedScale * 100) / 100); // �ε� �Ҽ��� ���� �ذ�
    }

    /// <summary>
    /// �߷� �Լ�
    /// <br/>
    /// �߷°��ӵ� = 9.81 m / s * s
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
    /// �ε巯�� �������� ���� �ڷ�ƾ �Լ�
    /// <br/>
    /// _moveSpeedScale ���� �ٲ�
    /// </summary>
    /// <param name="startValue">�ʱ� ��</param>
    /// <param name="endValue">�޼� ��</param>
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
