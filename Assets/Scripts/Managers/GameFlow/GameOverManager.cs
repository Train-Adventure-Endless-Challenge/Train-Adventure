using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject _gameoverUIPanel;

    public Action _gameOverEvent;       // GameOver상태일때의 event

    // Start is called before the first frame update
    void Start()
    {
        _gameoverUIPanel.SetActive(false);

        Init();
    }


    private void Init()
    {
        _gameOverEvent += SetGameOverPanel;
    }


    /// <summary>
    /// 게임이 종료됐을때 event 실행
    /// InGameManager에서 호출
    /// </summary>
    public void GameOver()
    {
        _gameOverEvent?.Invoke();
    }

    /// <summary>
    /// UI Set 
    /// _gameOverEvent로 호출하여 실행
    /// </summary>
    public void SetGameOverPanel()
    {
        _gameoverUIPanel.SetActive(true);
        _gameoverUIPanel.GetComponent<GameOverUI>().ScoreInit(InGameManager.Instance.Score);
    }

   
}
