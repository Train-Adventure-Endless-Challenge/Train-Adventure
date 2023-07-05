using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    /// <summary>
    /// 다시 시작 버튼 
    /// GameOver UI 에 존재
    /// </summary>
    public void RestartBtnClick()
    {
        Debug.Log("게임 재시작");
        SoundManager.Instance.PlayButtonClickSound();                           // 버튼 클릭 효과음 재생
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       // 현재 씬 재시작
    }
}
