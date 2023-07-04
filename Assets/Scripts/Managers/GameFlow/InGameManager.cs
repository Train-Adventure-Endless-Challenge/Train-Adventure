using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : SceneSingleton<InGameManager>
{
    [SerializeField] private Transform _backgroundGroup;

    [Header("Train")]
    [SerializeField] private GameObject _tutorialTrain;
    [SerializeField] private GameObject[] _nomalTrainObjects;
    [SerializeField] private GameObject _bossTrainObject;
    [SerializeField] private GameObject _storeTrainObject;
    
    [Header("UI")]
    [SerializeField] private Image fadeImage;  // 페이드 이미지

    private Train _currentTrain;               // 현재 기차       
    private Train _nextTrain;                  // 다음 기차

    private Vector3 _trainInterval = new Vector3(0, 0, 1.5f);   // 기차 간격
    private Vector3 _startPosition = Vector3.zero;

    private int score;

    [SerializeField] GameOverManager _gameOverManager;

    private void Start()
    {
        _currentTrain = CreateTrain(_tutorialTrain, _startPosition);
        _currentTrain.Init();

        _nextTrain = CreateTrain(_nomalTrainObjects[Random.Range(0, _nomalTrainObjects.Length)], _startPosition);

        _nextTrain.transform.position += new Vector3(0, 0, 
            (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _trainInterval;

        PlayerManager.Instance.gameObject.transform.position = _currentTrain._playerSpawnPoint.position;
    }

    /// <summary>
    /// 다음 스테이지로 이동하는 함수
    /// </summary>
    public void NextStage()
    {
        IngameUIController.Instance.UpdateScore(++score);
        ShakeManager.Instance.IncreaseShake(1f);            // 흔들림 증가 

        GameObject nextTrain = _nomalTrainObjects[Random.Range(0, _nomalTrainObjects.Length)];

        if (score == 4)
        {
            nextTrain = _storeTrainObject;
        }

        StartNextTrain(nextTrain);

        StartCoroutine(FadeInOutCor(1.5f, 1, 0));
    }

    /// <summary>
    /// 다음 기차로 이동했을 때 함수
    /// </summary>
    /// <param name="train">생성할 기차</param>
    public void StartNextTrain(GameObject train)
    {
        _currentTrain.DestroyGameObejct();
        _currentTrain = _nextTrain;
        _currentTrain.Init();

        _nextTrain = CreateTrain(train, _currentTrain.transform.position);

        // 두 개의 기차 칸의 바닥 절반의 크기와 trainInterval 더해 nextPosition을 구함
        Vector3 nextPosition = new Vector3(0, 0, (_nextTrain._floor.transform.localScale.z / 2)
            + (_currentTrain._floor.transform.localScale.z / 2)) + _trainInterval;
        
        _nextTrain.transform.position += nextPosition;

        _backgroundGroup.position += new Vector3(0, 0, nextPosition.z);
    }

    /// <summary>
    /// 기차 생성 함수
    /// </summary>
    /// <param name="train">생성할 기차</param>
    /// <param name="position">위치</param>
    /// <returns></returns>
    private Train CreateTrain(GameObject train , Vector3 position)
    {
        return Instantiate(train, position, Quaternion.identity).GetComponent<Train>();
    }

    /// <summary>
    /// Fade 코루틴
    /// </summary>
    /// <param name="time">페이드 되는 시간</param>
    /// <param name="start">시작하는 알파 값</param>
    /// <param name="end">끝나는 알파 값</param>
    /// <returns></returns>
    private IEnumerator FadeInOutCor(float time, float start, float end)
    {
        float current = 0;
        float percent = 0;

        fadeImage.color = new Color(0, 0, 0, start);

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;

            PlayerManager.Instance.StopMove();
            PlayerManager.Instance.gameObject.transform.position = _currentTrain._playerSpawnPoint.position;

            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, end);
    }

    /// <summary>
    /// 게임 오버 시 호출 함수
    /// </summary>
    public void GameOver()
    {
        _gameOverManager.GameOver();
    }
}

