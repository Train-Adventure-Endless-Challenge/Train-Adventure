using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    [Header("Curve")]
    [SerializeField] private AnimationCurve _sliderAnimationCurve; // Slider에서 시간에 따른 변화량을 사용하기 위한 커브

    [Header("Sprites")]
    [SerializeField] private Sprite[] _skillSprites; // 스킬 이미지 Sprite
    
    Coroutine _hpUpdateLerpCoroutine;       
    Coroutine _staminaUpdateLerpCoroutine;
    Coroutine _gearUpdateCoroutine;
    Coroutine _skillUIUpdateCoroutine;
    Coroutine _durabilityUpdateLerpCoroutine;

    private int _maxPopupCount = 9;

    private const float _hpSliderLerpTime = 1f;                // 체력 Lerp 목표 시간                 
    private const float _staminaRecoverySliderLerpTime = 0.1f; // 스태미나 회복 Lerp 목표 시간
    private const float _staminaUseSliderLerpTime = 0.3f;      // 스태미나 사용 Lerp 목표 시간
    private const float _durabilitySliderLerpTime = 1f;        // 내구도 Lerp 목표 시간

    /// <summary>
    /// HP가 변화했을 때 UI를 업데이트 시켜주는 함수
    /// </summary>
    /// <param name="hp">변경할 hp</param>
    /// <param name="maxHp">혹시 maxHp가 바뀌었다면 사용할 최대 체력</param>
    public void UpdateHpUI(float hp, float maxHp)
    {
        if (_hpSlider.maxValue != maxHp) // Hp Slider의 최댓값과 플레이어의 최대 체력이 다를 때
        {
            float hpSliderMaxValue = _hpSlider.maxValue;   // Hp Slider의 최댓값 초기화
            _hpSlider.maxValue = maxHp;                    // Hp Slider의 최댓값을 최대 체력의 값으로 변경
            _hpSlider.value *= (maxHp / hpSliderMaxValue); // 현재 Hp Slider의 값을 Hp Slider의 변경된 최댓값 비율로 변경
        }

        _hpUpdateLerpCoroutine = StartCoroutine(UpdateHpUILerpCor(hp)); // Hp UI 러프 시작
    }

    /// <summary>
    /// Hp UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="hp">목표 수치</param>
    /// <param name="isHpMax">최대 체력인지</param>
    /// <returns></returns>
    IEnumerator UpdateHpUILerpCor(float hp)
    {
        float startHp = _hpSlider.value; // 현재 Hp Slider의 값을 시작 값으로 초기화
        float currentTime = 0f;          // 현재 시간 초기화

        while (currentTime != _hpSliderLerpTime) // Lerp 목표 시간과 현재 시간이 같아질 때까지
        {
            currentTime += Time.deltaTime; // 현재 시간 더하기

            if (currentTime > _hpSliderLerpTime) // Lerp 목표 시간을 넘었을 때
            {
                currentTime = _hpSliderLerpTime; // 시간 조정
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / _hpSliderLerpTime); // 현재 시간에 대한 진행 비율의 애니메이션 커브 값 초기화
            float lerpValue = Mathf.Lerp(startHp, hp, curveValue);                              // 현재 시간에 대한 진행 비율의 러프 값 초기화

            _hpSlider.value = lerpValue;                             // Hp Slider를 러프 값 비율로 변경
            _curretnHpText.text = Mathf.Round(lerpValue).ToString(); // 현재 Hp Text를 러프 값의 반올림 값으로 변경

            yield return new WaitForEndOfFrame(); // 프레임이 종료 될 때 종료되도록 설정
        }

        _hpUpdateLerpCoroutine = null; // 코루틴 초기화
    }

    /// <summary>
    /// stamina가 변경되었을 때 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="stamina">목표 스테미나</param>
    /// <param name="maxStamina">혹시 모를 최대 stamina 변경 시 사용할 변수</param>
    public void UpdateStaminaUI(float stamina, float maxStamina)
    {
        if (_staminaSlider.maxValue != maxStamina) // Stamina Slider의 최댓값과 플레이어의 최대 스태미나가 다를 때
        {
            float staminaSliderMaxValue = _staminaSlider.maxValue;        // Stamina Slider의 최댓값 담기
            _staminaSlider.maxValue = maxStamina;                         // Stamina Slider의 최댓값을 최대 스태미나의 값으로 변경
            _staminaSlider.value *= (maxStamina / staminaSliderMaxValue); // 현재 Stamina Slider의 값을 Stamina Slider의 변경된 최댓값 비율로 변경
        }

        _staminaUpdateLerpCoroutine = StartCoroutine(UpdateStaminaUILerpCor(stamina)); // Stamina UI 러프 시작
    }

    /// <summary>
    /// stamina UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="stamina">목표 stamina</param>
    /// <returns></returns>
    IEnumerator UpdateStaminaUILerpCor(float stamina)
    {
        float startStamina = _staminaSlider.value; // 현재 Stamina Slider의 값을 시작 값으로 초기화
        float currentTime = 0f;                    // 현재 시간 초기화
        float staminaSliderLerpTime = 0;           // Lerp 목표 시간 초기화

        if (stamina > startStamina) staminaSliderLerpTime = _staminaRecoverySliderLerpTime; // 스태미나 회복이라면 Lerp 목표 시간을 Lerp 회복 목표 시간으로 초기화
        else staminaSliderLerpTime = _staminaUseSliderLerpTime;                             // 스태미나 사용이라면 Lerp 목표 시간을 Lerp 사용 목표 시간으로 초기화

        while (currentTime != staminaSliderLerpTime) // Lerp 목표 시간과 현재 시간이 같아질 때까지
        {
            currentTime += Time.deltaTime; // 현재 시간 더하기

            if (currentTime > staminaSliderLerpTime) // 목표 시간을 넘었을 때
            {
                currentTime = staminaSliderLerpTime; // 시간 조정
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / staminaSliderLerpTime); // 현재 시간에 대한 진행 비율의 애니메이션 커브 값 초기화
            float lerpValue = Mathf.Lerp(startStamina, stamina, curveValue);                        // 현재 시간에 대한 진행 비율의 러프 값 초기화

            _staminaSlider.value = lerpValue;                             // Stamina Slider를 러프 값 비율로 변경
            _currentStaminaText.text = Mathf.Round(lerpValue).ToString(); // 현재 Stamina Text 러프 값의 반올림 값으로 변경

            yield return new WaitForEndOfFrame(); // 프레임이 종료 될 때 종료되도록 설정
        }

        _staminaUpdateLerpCoroutine = null; // 코루틴 초기화
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
    /// 흔들림 수치 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="shakeAmount">목표 흔들림 수치</param>
    public void UpdateShakeUI(float endShakeAmount)
    {
        StartCoroutine(UpdateShakeUICor(_pointerImage.transform.eulerAngles.z, endShakeAmount * 36f));

        _shakeAmountText.text = (Mathf.Round(endShakeAmount * 10) / 10).ToString(); // 소수 첫 번째 자리까지 흔들림 수치가 변경되도록 설정
    }

    /// <summary>
    /// 흔들림 수치 Pointer 방향을 자연스럽게 돌리는 코루틴
    /// </summary>
    /// <param name="start">시작 수치</param>
    /// <param name="end">목표 수치</param>
    /// <returns></returns>
    private IEnumerator UpdateShakeUICor(float start, float end)
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

        _skillImg.sprite = _skillSprites[weaponIndex]; // 스킬 이미지 변경

        _skillUIUpdateCoroutine = StartCoroutine(UpdateSkillUICor(coolTime, currentTime));
    }

    IEnumerator UpdateSkillUICor(float coolTime, float currentTime)
    {
        while (Time.time < currentTime)
        {
            yield return null;
            _skillCooltimeImg.fillAmount = ((currentTime - Time.time) / coolTime);
        }

        _skillCooltimeImg.fillAmount = 0;

        _skillUIUpdateCoroutine = null;
    }

    public void OnDurabilityUI(bool toggle)
    {
        _durabilitySlider.gameObject.SetActive(toggle);
    }

    /// <summary>
    /// 내구도 UI를 업데이트하는 함수
    /// </summary>
    /// <param name="maxDurability">최대 내구도</param>
    /// <param name="durability">현재 내구도</param>
    public void UpdateDurabilityUI(float maxDurability, float durability)
    {
        if (_durabilitySlider.maxValue != maxDurability) // Durability Slider의 최댓값과 무기의 최대 내구도가 다를 때
        {
            float durabilitySliderMaxValue = _durabilitySlider.maxValue;           // Durability Slider의 최댓값 초기화
            _durabilitySlider.maxValue = maxDurability;                            // Durability Slider의 최댓값을 최대 내구도의 값으로 변경 
            _durabilitySlider.value *= (maxDurability / durabilitySliderMaxValue); // 현재 Durability Slider의 값을 Durability Slider의 변경된 최댓값 비율로 변경
        }

        _durabilityUpdateLerpCoroutine = StartCoroutine(UpdateDurabilityUILerpCor(durability)); // Durability UI 러프 시작
    }

    /// <summary>
    /// Durability UI를 Lerp하게 바꾸는 함수
    /// </summary>
    /// <param name="durability">목표 durability</param>
    /// <returns></returns>
    private IEnumerator UpdateDurabilityUILerpCor(float durability)
    {
        float startDurability = _durabilitySlider.value; // 현재 Durability Slider의 값을 시작 값으로 초기화
        float currentTime = 0f;                          // 현재 시간 초기화

        while (currentTime != _durabilitySliderLerpTime) // Lerp 목표 시간과 현재 시간이 같아질 때까지
        {
            currentTime += Time.deltaTime; // 현재 시간 더하기

            if (currentTime > _durabilitySliderLerpTime) // Lerp 목표 시간을 넘었을 때
            {
                currentTime = _durabilitySliderLerpTime; // 시간 조정
            }

            float curveValue = _sliderAnimationCurve.Evaluate(currentTime / _durabilitySliderLerpTime);  // 현재 시간에 대한 진행 비율의 애니메이션 커브 값 초기화
            float lerpValue = Mathf.Lerp(startDurability, durability, curveValue);                       // 현재 시간에 대한 진행 비율의 러프 값 초기화

            _durabilitySlider.value = lerpValue;                             // Hp Slider를 러프 값 비율로 변경
            _currentDurabilityText.text = Mathf.Round(lerpValue).ToString(); // 현재 Hp Text를 러프 값의 반올림 값으로 변경

            yield return new WaitForEndOfFrame(); // 프레임이 종료 될 때 종료되도록 설정
        }

        _durabilityUpdateLerpCoroutine = null; // 코루틴 초기화
    }
}
