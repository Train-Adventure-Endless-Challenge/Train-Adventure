using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : SceneSingleton<IngameUIController>
{

    [SerializeField] Slider _hpSlider;
    [SerializeField] Slider _steminaSlider;


    Coroutine _hpUpdateCoroutine;
    public void UpdateHp(float hp, float maxHp)
    {
        _hpSlider.maxValue = maxHp;

        if (_hpUpdateCoroutine != null)
            StopCoroutine(_hpUpdateCoroutine);

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

    IEnumerator UpdateHpCor(float hp)
    {
        while (true)
        {
            yield return null;

            Debug.Log($"UPDate HP: {_hpSlider.value} -> {hp}");
            _hpSlider.value = Mathf.Lerp(_hpSlider.value, hp, 10 * Time.deltaTime);

            if (Mathf.Abs(_hpSlider.value - hp) <= 0.1f)
            {
                _hpUpdateCoroutine = null;
                break;

            }
        }
    } 
}
