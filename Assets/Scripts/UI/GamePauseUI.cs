using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private KeyCode _pauseKeyCode;

    private void Update()
    {
        /*if (Input.GetKeyDown(_pauseKeyCode)) 
        {
            OnPausePanel();
        }*/
    }

    public void OnPausePanel()
    {
        if (_pausePanel.activeSelf == false)
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
        LoadSceneController.LoadScene(0);
    }
}
