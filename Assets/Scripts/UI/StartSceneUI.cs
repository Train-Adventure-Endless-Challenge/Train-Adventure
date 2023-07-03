using UnityEngine;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private int _gameSceneCount;

    public void OnStartButton()
    {
        LoadSceneController.LoadScene(_gameSceneCount);
    }
}
