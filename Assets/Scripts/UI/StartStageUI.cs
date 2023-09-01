using TMPro;
using UnityEngine;
using System.Collections;

public class StartStageUI : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _waitTime = 2f; // 애니메이션 대기 시간

    [Header("UI")]
    [SerializeField] private TMP_Text _stageText; // 스테이지 시작 값 텍스트

    #endregion

    #region Function

    #region LifeCycle

    private void OnEnable()
    {
        InGameManager inGameManager = InGameManager.Instance;   

        if (inGameManager.BossIndex == inGameManager.Score)
        {
            _stageText.text = "Boss Stage";
        }
        else if (inGameManager.StoreIndex == inGameManager.Score)
        {
            _stageText.text = "Store";
        }
        else
        {
            _stageText.text = "Stage " + inGameManager.Score.ToString(); // 스테이지 값 받아오기
        }
        StartCoroutine(DisableCoroutine());                             // 시간 대기 후 비활성화 시작
    }

    #endregion

    /// <summary>
    /// 시간 대기 후 비할성화 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(_waitTime); // 애니메이션 시간 대기
        gameObject.SetActive(false);                // 비활성화
    }

    #endregion
}
