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

    Coroutine _hpUpdateCoroutine;
    Coroutine _staminaUpdateCoroutine;
    Coroutine _gearUpdateCoroutine;
    public void UpdateHp(float hp, float maxHp)
    {
        if (_hpSlider.maxValue != maxHp)
        {
            _hpSlider.maxValue = maxHp;
            _hpSlider.value = maxHp;
        }
        if (_hpUpdateCoroutine != null)
            StopCoroutine(_hpUpdateCoroutine);

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

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

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateGear(int gear)
    {


        if (_gearUpdateCoroutine != null)
            StopCoroutine(_gearUpdateCoroutine);

        _gearUpdateCoroutine = StartCoroutine(TextCountCor(_gearText, gear, float.Parse(_gearText.text)));
            

    }

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

}
