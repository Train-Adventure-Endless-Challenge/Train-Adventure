using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public void ScoreInit(int score)
    {
        _scoreText.text = score.ToString();
    }

    /// <summary>
    /// 다시 시작 버튼 
    /// GameOver UI 에 존재
    /// </summary>
    public void OnClickRestartBtn()
    {
        SoundManager.Instance.PlayButtonClickSound();                           // 버튼 클릭 효과음 재생
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       // 현재 씬 재시작
    }

    public void OnClickMenuBtn()
    {
        SoundManager.Instance.PlayButtonClickSound();                           // 버튼 클릭 효과음 재생
        SceneManager.LoadScene("TitleScene");                                   // 타이틀 씬으로
    }
}
