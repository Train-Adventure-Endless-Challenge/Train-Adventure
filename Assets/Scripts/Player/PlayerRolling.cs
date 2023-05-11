﻿using System.Collections;
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

    private void Update()
    {
        if (Input.GetKeyDown(_rollingKey) && _rollCor == null)
        {
            StartCoroutine(RollCor(transform.position));
        }
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
    public void Roll()
    {
        if (Input.GetKeyDown(_rollingKey) && _rollCor == null)
        {
            _player.playerState = PlayerState.Rolling;
            _rollCor = RollCor(transform.position);
            StartCoroutine(RollCor(transform.position));
        }
    }

    /// <summary>
    /// 구르기 코루틴 함수
    /// </summary>
    /// <param name="startPosition">초기 값</param>
    /// <returns></returns>
    public IEnumerator RollCor(Vector3 startPosition)
    {
        _currentTime = 0f;
        _lerpTime = 1f;
        _characterController.detectCollisions = false;
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
        _characterController.detectCollisions = true;
        _animator.SetBool("IsRoll", false);
        _animator.SetTrigger("OnState");
        _rollCor = null;
        _player.playerState = PlayerState.Idle;
    }

    #endregion
}
