using UnityEngine;

public class PlayerController : SceneSingleton<PlayerController>
{
    #region Variable

    #region SerializeField
    [Header("Key")]
    [SerializeField] private KeyCode _runningKey;
    #endregion

    #region Private



    private Player _player;
    private Animator _animator;
    private CharacterController _controller;

    private Vector3 _velocity;
    private Vector3 _moveDirection;

    private float _speed;
    private float _moveSpeedScale;
    private float _speedScale = 0.5f;
    private float _runSpeedScale = 1;
    private float _gravity = -9.81f;

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
        Gravity();
    }

    private void Start()
    {
        DataInit();
    }

    #endregion

    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
    }

    private void DataInit()
    {
        _speed = _player.Speed;
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            if (Input.GetKey(_runningKey))
            {
                _moveSpeedScale = _runSpeedScale;
            }
            else _moveSpeedScale = _speedScale;
            _moveDirection = new Vector3(x, 0, z);
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
            _moveSpeedScale = 0;
            if (_player.playerState != PlayerState.Idle)
            {
                _animator.SetTrigger("OnState");
                _player.playerState = PlayerState.Idle;
            }
        }
        _animator.SetFloat("MoveSpeed", _moveSpeedScale);
    }

    private void Gravity()
    {
        if (_controller.isGrounded == false)
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0;
        }
        _controller.Move(Time.deltaTime * _velocity);
    }
    #endregion
}
