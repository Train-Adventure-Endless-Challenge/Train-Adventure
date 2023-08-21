using TMPro;
using UnityEngine;
using System.Collections;

public class StartStageUI : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _waitTime = 2f; // 애니메이션 대기 시간

    [Header("UI")]
    [SerializeField] private TMP_Text _stageValueText; // 스테이지 시작 값 텍스트

    #endregion

    #region Function

    #region LifeCycle

    private void OnEnable()
    {
        _stageValueText.text = InGameManager.Instance.Score.ToString(); // 스테이지 값 받아오기
        StartCoroutine(DisableCoroutine());                             // 시간 대기 후 비활성화 코루틴
    }

    #endregion

    /// <summary>
    /// 대기 후 비할성화 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(_waitTime); // 애니메이션 시간 대기
        gameObject.SetActive(false);                // 비활성화
    }

    #endregion
}
