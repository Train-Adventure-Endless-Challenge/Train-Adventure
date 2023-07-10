// 작성자 : 박재만
// 작성일 : 2023-07-03

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬 로드 조작을 담당하는 클래스
/// </summary>
public class LoadSceneController : MonoBehaviour
{
    #region Variable

    [SerializeField] private float _minProgressValue = 0.9f; // 로딩전 최소 씬 진행 정도

    [SerializeField] private Slider _progressSlider; // 로딩 슬라이더 바

    private static int _loadingScene = 1; // 로딩 씬 인덱스 번호
    static int _nextSceneNumber;          // 로딩 될 씬 인덱스 번호

    #endregion

    #region Function

    /// <summary>
    /// 씬 조작을 담당하는 함수
    /// </summary>
    /// <param name="sceneNumber">로딩 될 씬 번호</param>
    public static void LoadScene(int sceneNumber)
    {
        _nextSceneNumber = sceneNumber;        // 번호 초기화
        SceneManager.LoadScene(_loadingScene); // 로딩 씬으로 이동
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgressCor()); // 씬 로드 진행
    }

    /// <summary>
    /// 씬 로드 진행을 담당하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneProgressCor()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneNumber); // 동적으로 씬을 불러옴
        op.allowSceneActivation = false;                                   // 미리 씬 넘어감 방지

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < _minProgressValue) // 진행이 거의 덜 됐을 때
            {
                _progressSlider.value = op.progress; // 로딩 슬라이더 값 변경
            }
            else // 로딩씬이 보이기 전에 바로 넘어가는 경우를 방지하기 위함
            {
                timer += Time.unscaledDeltaTime;
                _progressSlider.value = Mathf.Lerp(0.9f, 1f, timer);
                if (_progressSlider.value >= 1f) // 씬이 전부 불러졌을 때
                {
                    op.allowSceneActivation = true; // 씬 불러오기
                    yield break;                    // 코루틴 종료
                }
            }
        }
    }

    #endregion
}