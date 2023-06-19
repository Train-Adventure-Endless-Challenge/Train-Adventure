// 작성자 : 박재만
// 작성일 : 2023-06-19

#region Namespace

using UnityEngine;
using System.Collections;

#endregion

/// <summary>
/// 플레이어 스태미나를 담당하는 클래스
/// <br/> 최대 스태미나는 100으로 0.5초 동안 변화가 없다면 초 당 20의 양을 회복
/// </summary>
public class PlayerStamina : MonoBehaviour
{
    #region Variable

    [SerializeField] private float _waitTime;     // 변화 대기 시간
    [SerializeField] private float _recoveryTime; // 회복 시간
    [SerializeField] private int _maxValue;        // 스테미너 최대값
    [SerializeField] private int _recoveryValue;    // 회복 시간당 회복량

    private IEnumerator _recoverCor;

    #region Class

    private Player _player;

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화
    }

    private void Start()
    {
        DataInit();
    }

    #endregion

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
    }

    private void DataInit()
    {
        _waitTime = _player.WaitTime;
        _recoveryTime = _player.RecoveryTime;
        _maxValue = _player.MaxValue;
        _recoveryValue = _player.RecoveryValue;
    }

    /// <summary>
    /// 스태미너를 회복하는 함수
    /// <br/> 중복 검사 후 진행
    /// </summary>
    public void Recover()
    {
        if (_recoverCor == null)
        {
            StartCoroutine(RecoverCor());
        }
    }

    /// <summary>
    /// 스태미너 회복을 멈추는 함수
    /// </summary>
    public void RecoverStop()
    {
        if (_recoverCor != null)
        {
            StopAllCoroutines();
            _recoverCor = null;
        }
    }

    /// <summary>
    /// 스태미너 회복 코루틴 함수
    /// <br/> 최대 스태미나는 100으로 0.5초 동안 변화가 없다면 초 당 20의 양을 회복
    /// </summary>
    /// <returns></returns>
    private IEnumerator RecoverCor()
    {
        _recoverCor = RecoverCor();
        yield return new WaitForSeconds(_waitTime); // 변화 대기
        while (true)
        {
            if (_player.Stamina + _recoveryValue > _maxValue) // 회복시 최댓값 일 때
            {
                _player.Stamina = _maxValue; // 최댓값 대입
            }
            else
            {
                _player.Stamina += _recoveryValue; // 회복 
            }
            IngameUIController.Instance.UpdateStamina(_player.Stamina, _player._maxStamina);       // ※추후 변경 예정※ UI 변경
            yield return new WaitForSeconds(_recoveryTime); // 회복 시간 대기
        }
    }

    #endregion
}
