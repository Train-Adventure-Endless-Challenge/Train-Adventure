using UnityEngine;
using UnityEngine.UI;

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
