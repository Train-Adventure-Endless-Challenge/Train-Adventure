using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor.Rendering;

public class IngameUIController : SceneSingleton<IngameUIController>
{
    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _staminaSlider;
    [SerializeField] Slider _durabilitySlider;

    [Header("UI")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _gearText;
    [SerializeField] TMP_Text _shakeAmountText;

    [SerializeField] Image _shakeAmountBackground;

    [SerializeField] private GameObject _pointerImage;

    [Header("Hp")]
    [SerializeField] private TMP_Text _curretnHpText;
    [SerializeField] private TMP_Text _maxHpText;

    [Header("Stamina")]
    [SerializeField] private TMP_Text _currentStaminaText;
    [SerializeField] private TMP_Text _maxStaminaText;

    [Header("Durability")]
    [SerializeField] private TMP_Text _currentDurabilityText;
    [SerializeField] private TMP_Text _maxDurabilityText;

    [Header("Popup")]
    [SerializeField] private GameObject _popupPanel;
    [SerializeField] private GameObject _popupText;

    [Header("Skill")]
    [SerializeField] private Image _skillImg;
    [SerializeField] private Image _skillCooltimeImg;

    [SerializeField] private AnimationCurve _sliderAnimationCurve;

    Coroutine _hpUpdateCoroutine;
    Coroutine _staminaUpdateCoroutine;
    Coroutine _gearUpdateCoroutine;
    Coroutine _skillUIUpdateCoroutine;
    Coroutine _durabilityUpdateCoroutine;

    [Header("Sprites")]
    [SerializeField] private Sprite[] _skillSprites;

    private int _maxPopupCount = 9;

    private const float _hpSliderLerpTime = 1f;
    private const float _staminaRecoverySliderLerpTime = 0.1f;
    private const float _staminaHitSliderLerpTime = 0.5f;
    private const float _durabilitySliderLerpTime = 1f;

    /// <summary>
    /// HP가 변화했을 때 UI를 업데이트 시켜주는 함수
    /// </summary>
    /// <param name="hp">변경할 hp</param>
    /// <param name="maxHp">혹시 maxHp가 바뀌었다면 사용할 최대 체력</param>
    public void UpdateHp(float hp, float maxHp)
    {
        if (_hpSlider.maxValue != maxHp)
        {
            float hpSliderMaxValue = _hpSlider.maxValue;
            _hpSlider.maxValue = maxHp;
            _hpSlider.value *= (maxHp / hpSliderMaxValue);
        }
        if (_hpUpdateCoroutine != null)
            StopCoroutine(_hpUpdateCoroutine);

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

    /// <summary>
    /// Hp UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="hp">목표 수치</param>
    /// <param name="isHpMax">최대 체력인지</param>
    /// <returns></returns>
    IEnumerator UpdateHpCor(float hp)
    {
        float startHp = _hpSlider.value;

        float currentTime = 0f;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _hpSliderLerpTime)
            {
                currentTime = _hpSliderLerpTime;
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / _hpSliderLerpTime);

            float lerpValue = Mathf.Lerp(startHp, hp, curveValue);

            _hpSlider.value = lerpValue;

            _curretnHpText.text = Mathf.Round(lerpValue).ToString();

            yield return new WaitForEndOfFrame();
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
            float staminaSliderMaxValue = _staminaSlider.maxValue;
            _staminaSlider.maxValue = maxStamina;
            _staminaSlider.value *= (maxStamina / staminaSliderMaxValue);
        }

        _staminaUpdateCoroutine = StartCoroutine(UpdateStaminaCor(stamina));
    }

    /// <summary>
    /// stamina UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="stamina">목표 stamina</param>
    /// <returns></returns>
    IEnumerator UpdateStaminaCor(float stamina)
    {
        float startStamina = _staminaSlider.value;
        float currentTime = 0f;
        float staminaSliderLerpTime = 0;

        if (stamina > startStamina) staminaSliderLerpTime = _staminaRecoverySliderLerpTime;
        else staminaSliderLerpTime = _staminaHitSliderLerpTime;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > staminaSliderLerpTime)
            {
                currentTime = staminaSliderLerpTime;
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / staminaSliderLerpTime);

            float lerpValue = Mathf.Lerp(startStamina, stamina, curveValue);

            _staminaSlider.value = lerpValue;

            _currentStaminaText.text = Mathf.Round(lerpValue).ToString();

            yield return new WaitForEndOfFrame();
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

        _shakeAmountText.text = (Mathf.Round(endShakeAmount * 10) / 10).ToString();
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
        if (_popupPanel.transform.childCount == _maxPopupCount)
            return;
        Instantiate(_popupText, _popupPanel.transform).GetComponent<PopupText>().Init(text);
    }

    public void UpdateSkillUI(int weaponIndex, float coolTime, float currentTime)
    {
        if (_skillUIUpdateCoroutine != null)
            StopCoroutine(_skillUIUpdateCoroutine);

        _skillUIUpdateCoroutine = StartCoroutine(UpdateSkillUICor(weaponIndex, coolTime, currentTime));
    }

    IEnumerator UpdateSkillUICor(int weaponIndex, float coolTime, float currentTime)
    {
        _skillImg.sprite = _skillSprites[weaponIndex];

        while (Time.time < currentTime)
        {
            yield return null;
            _skillCooltimeImg.fillAmount = ((currentTime - Time.time) / coolTime);
        }

        _skillCooltimeImg.fillAmount = 0;

        _skillUIUpdateCoroutine = null;
    }

    public void OnDurability(bool toggle)
    {
        _durabilitySlider.gameObject.SetActive(toggle);
    }

    public void UpdateDurability(float maxDurability, float durability)
    {
        if (_durabilitySlider.maxValue != maxDurability)
        {
            float durabilitySliderMaxValue = _durabilitySlider.maxValue;
            _durabilitySlider.maxValue = maxDurability;
            _durabilitySlider.value *= (maxDurability / durabilitySliderMaxValue);
        }

        _durabilityUpdateCoroutine = StartCoroutine(UpdateDurabilityCor(durability));
    }

    private IEnumerator UpdateDurabilityCor(float durability)
    {
        float startDurability = _durabilitySlider.value;
        float currentTime = 0f;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _durabilitySliderLerpTime)
            {
                currentTime = _durabilitySliderLerpTime;
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / _durabilitySliderLerpTime);

            float lerpValue = Mathf.Lerp(startDurability, durability, curveValue);

            _durabilitySlider.value = lerpValue;

            _currentDurabilityText.text = Mathf.Round(lerpValue).ToString();

            yield return new WaitForEndOfFrame();
        }
    }
}
