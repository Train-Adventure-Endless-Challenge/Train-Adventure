using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : SceneSingleton<IngameUIController>
{
    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _staminaSlider;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _gearText;
    [SerializeField] Image _shakeAmountBackground;
    [SerializeField] private GameObject _pointerImage;

    [Header("Popup")]
    [SerializeField] private GameObject _popupPanel;
    [SerializeField] private GameObject _popupText;

    Coroutine _hpUpdateCoroutine;
    Coroutine _staminaUpdateCoroutine;
    Coroutine _gearUpdateCoroutine;

    /// <summary>
    /// HP가 변화했을 때 UI를 업데이트 시켜주는 함수
    /// </summary>
    /// <param name="hp">변경할 hp</param>
    /// <param name="maxHp">혹시 maxHp가 바뀌었다면 사용할 최대 체력</param>
    public void UpdateHp(float hp, float maxHp)
    {
        if (_hpSlider.maxValue != maxHp)
        {
            _hpSlider.maxValue = maxHp;
        }
        if (_hpUpdateCoroutine != null)
            StopCoroutine(_hpUpdateCoroutine);

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

    /// <summary>
    /// Hp UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="hp">목표 수치</param>
    /// <returns></returns>
    IEnumerator UpdateHpCor(float hp)
    {
        while (true)
        {
            yield return null;

            _hpSlider.value = Mathf.Lerp(_hpSlider.value, hp, 10 * Time.deltaTime);

            if (Mathf.Abs(_hpSlider.value - hp) <= 0.1f)
            {
                _hpUpdateCoroutine = null;
                break;
            }
        }
    }

    /// <summary>
    /// stamina가 변경되었을 때 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="stamina">목표 스테미나</param>
    /// <param name="maxStamina">혹시 모를 최대 stamina 변경 시 사용할 변수</param>
    public void UpdateStamina(float stamina, float maxStamina)
    {

        if (_staminaSlider.maxValue != maxStamina)
        {
            _staminaSlider.maxValue = maxStamina;
        }
        if (_staminaUpdateCoroutine != null)
            StopCoroutine(_staminaUpdateCoroutine);

        _staminaUpdateCoroutine = StartCoroutine(UpdateSteminaCor(stamina));
    }

    /// <summary>
    /// stamina UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="stamina">목표 stamina</param>
    /// <returns></returns>
    IEnumerator UpdateSteminaCor(float stamina)
    {
        while (true)
        {
            yield return null;

            _staminaSlider.value = Mathf.Lerp(_staminaSlider.value, stamina, 10 * Time.deltaTime);

            if (Mathf.Abs(_staminaSlider.value - stamina) <= 0.1f)
            {
                _staminaUpdateCoroutine = null;
                break;
            }
        }
    }

    /// <summary>
    /// 점수 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="score">바꿀 점수 수치</param>
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    /// <summary>
    /// 기어 수 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="gear">변경할 gear수</param>
    public void UpdateGear(int gear)
    {
        if (_gearUpdateCoroutine != null)
            StopCoroutine(_gearUpdateCoroutine);

        _gearUpdateCoroutine = StartCoroutine(TextCountCor(_gearText, gear, float.Parse(_gearText.text)));
    }

    /// <summary>
    /// 수치 text를 자연스레 올라가게하는 코루틴
    /// </summary>
    /// <param name="text">변경할 Text</param>
    /// <param name="target">목표 수치</param>
    /// <param name="current">현재 수치</param>
    /// <returns></returns>
    private IEnumerator TextCountCor(TMP_Text text, float target, float current)
    {
        float duration = 0.2f; // 카운팅에 걸리는 시간 설정

        float offset = (target - current) / duration;

        while (current < target)
        {
            current += offset * Time.deltaTime;

            text.text = ((int)current).ToString();

            yield return null;
        }

        current = (int)target;

        text.text = current.ToString();

        _gearUpdateCoroutine = null;
    }

    /// <summary>
    /// 흔들림 수치를 업데이트하는 함수
    /// </summary>
    /// <param name="shakeAmount">목표 흔들림 수치</param>
    public void UpdateShakeAmount(float endShakeAmount)
    {
        StartCoroutine(UpdateShakeAmountCor(_pointerImage.transform.eulerAngles.z, endShakeAmount * 36f));
    }

    /// <summary>
    /// 흔들림 수치 Pointer 방향을 자연스럽게 돌리는 코루틴
    /// </summary>
    /// <param name="start">시작 수치</param>
    /// <param name="end">목표 수치</param>
    /// <returns></returns>
    private IEnumerator UpdateShakeAmountCor(float start, float end)
    {
        float time = 1f;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            float zRotation = Mathf.Lerp(start, end, percent) % 360.0f;

            _shakeAmountBackground.fillAmount = Mathf.Lerp(start, end, percent) / 360f;

            _pointerImage.transform.eulerAngles = 
                new Vector3(_pointerImage.transform.eulerAngles.x, _pointerImage.transform.eulerAngles.y, zRotation);

            yield return null;
        }

        _pointerImage.transform.eulerAngles =
            new Vector3(_pointerImage.transform.eulerAngles.x, _pointerImage.transform.eulerAngles.y, end);

        _shakeAmountBackground.fillAmount = end / 360.0f;
    }

    public void PopupText(string text)
    {
        if (_popupPanel.transform.childCount == 9)
            return;
        Instantiate(_popupText, _popupPanel.transform).GetComponent<PopupText>().Init(text);
    }
}
