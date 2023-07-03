using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    private static int _loadingScene = 2;
    static int _nextSceneNumber;

    [SerializeField] private Slider _progressSlider;

    public static void LoadScene(int sceneNumber)
    {
        _nextSceneNumber = sceneNumber;
        SceneManager.LoadScene(_loadingScene);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgressCor());
    }

    private IEnumerator LoadSceneProgressCor()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneNumber);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                _progressSlider.value = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _progressSlider.value = Mathf.Lerp(0.9f, 1f, timer);
                if (_progressSlider.value >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}