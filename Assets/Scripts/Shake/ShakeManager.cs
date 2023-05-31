public class ShakeManager : SceneSingleton<TrainManager>
{
    private Shake _shake;
    private IncreaseShake _increaseShake;

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
}