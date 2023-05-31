/// <summary>
/// 흔들림을 관리하는 클래스
/// </summary>
public class ShakeManager : SceneSingleton<TrainManager>
{
    #region Variable

    private Shake _shake;
    private IncreaseShake _increaseShake;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        _shake = GetComponent<Shake>();
        _increaseShake = GetComponent<IncreaseShake>();
    }

    private void Start()
    {
        _shake.StartShake();
        _increaseShake.StartIncreaseShake();
    }

    #endregion

    #endregion
}