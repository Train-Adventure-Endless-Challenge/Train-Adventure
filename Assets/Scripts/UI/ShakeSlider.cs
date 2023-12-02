using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 흔들림 슬라이더를 담당하는 클래스 
/// <br/> ※추후 변경※
/// </summary>
public class ShakeSlider : MonoBehaviour
{
    #region Variable

    [Header("UI")]
    [SerializeField] private Text _text;

    private Slider _slider;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화
    }

    #endregion

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _slider = GetComponent<Slider>();
    }

    /// <summary>
    /// UI를 변경하는 함수
    /// </summary>
    /// <param name="value">흔들림 강도</param>
    public void ChangeUI(float value)
    {
        _slider.value = value / 10;
        _text.text = value.ToString();
    }

    #endregion
}
