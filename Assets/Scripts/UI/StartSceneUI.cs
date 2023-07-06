// 작성자 : 박재만
// 작성일 : 2023-07-03

using UnityEngine;

/// <summary>
/// 시작 씬 Canvas UI를 담당하는 함수
/// </summary>
public class StartSceneUI : MonoBehaviour
{
    #region Variable

    [SerializeField] private int _gameSceneCount; // 게임 씬 인덱스 번호

    #endregion

    #region Function

    /// <summary>
    /// 게임 시작 버튼을 눌렀을 때
    /// </summary>
    public void OnStartButton()
    {
        LoadSceneController.LoadScene(_gameSceneCount); // 게임 씬 로딩
    }

    public void OnQuitButton()
    {
        Application.Quit(); 
    }

    #endregion
}
