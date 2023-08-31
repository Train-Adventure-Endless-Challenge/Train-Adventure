public class CameraManager : SceneSingleton<CameraManager>
{
    #region Variable

    private CameraController _cameraController; // 카메라 조작 클래스

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    #endregion

    /// <summary>
    /// 초기화를 담당하는 함수
    /// </summary>
    private void Init()
    {
        _cameraController = GetComponent<CameraController>(); // 카메라 조작 클래스 초기화
    }

    /// <summary>
    /// 줌 실행을 연결하는 함수
    /// </summary>
    public void Joom(bool isSkill)
    {
        _cameraController.Joom(isSkill); // 카메라 줌 실행
    }

    #endregion
}
