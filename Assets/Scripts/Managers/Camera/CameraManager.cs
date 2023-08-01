public class CameraManager : SceneSingleton<CameraManager>
{
    private CameraController _cameraController;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cameraController = GetComponent<CameraController>();
    }

    public void Joom()
    {
        _cameraController.Joom();
    }
}
