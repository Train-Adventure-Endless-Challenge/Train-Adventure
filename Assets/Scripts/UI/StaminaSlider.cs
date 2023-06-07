using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스태미나 슬라이더를 담당하는 클래스
/// <br/> ※추후 변경※
/// </summary>
public class StaminaSlider : MonoBehaviour
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
        Init();
    }

    private void Init()
    {
        _slider = GetComponent<Slider>();
    }

    #endregion
    
    public void ChangeUI(float value)
    {
        _slider.value = value / 100;
        _text.text = value.ToString();
    }

    #endregion
}
