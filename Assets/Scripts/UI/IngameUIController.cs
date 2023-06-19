using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : SceneSingleton<IngameUIController>
{

    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _steminaSlider;


    Coroutine _hpUpdateCoroutine;
    Coroutine _steminaUpdateCoroutine;
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

    public void UpdateStemina(float stemina, float maxStemina)
    {
        if (_hpSlider.maxValue != maxStemina)
        {
            _steminaSlider.maxValue = maxStemina;
            _steminaSlider.value = maxStemina;
        }
        if (_steminaUpdateCoroutine != null)
            StopCoroutine(_steminaUpdateCoroutine);

        _steminaUpdateCoroutine = StartCoroutine(UpdateSteminaCor(stemina));
    }

    IEnumerator UpdateSteminaCor(float stemina)
    {
        while (true)
        {
            yield return null;

            _steminaSlider.value = Mathf.Lerp(_hpSlider.value, stemina, 10 * Time.deltaTime);

            if (Mathf.Abs(_steminaSlider.value - stemina) <= 0.1f)
            {
                _hpUpdateCoroutine = null;
                break;

            }
        }
    }
}
