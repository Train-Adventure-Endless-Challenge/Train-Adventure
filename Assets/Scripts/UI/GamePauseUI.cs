using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private KeyCode _pauseKeyCode;

    private void Update()
    {
        if (_pausePanel.activeSelf == false && Input.GetKeyDown(_pauseKeyCode))
        {
            _pausePanel.SetActive(true);
        }
    }

    public void OnClickResumeButton()
    {
        _pausePanel.SetActive(false);
    }

    public void OnClickMenuButton()
    {
        SceneManager.LoadScene("TitleScene"); 
    }
}
