using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private EnemyController _enemyController;
    public Slider _hpBarSlider;

    private Coroutine _hpUpdateCoroutine;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        Debug.Log(_enemyController.MaxHp);
    }

    public void Init()
    {
        _hpBarSlider.maxValue = _enemyController.MaxHp;
        _hpBarSlider.value = _enemyController.MaxHp;
        Debug.Log(_enemyController.MaxHp);
        _hpBarSlider.gameObject.SetActive(false);
    }

    public void UpdateHpUI(float hp)
    {
        if (_hpUpdateCoroutine != null)
            StopCoroutine(_hpUpdateCoroutine);

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

    IEnumerator UpdateHpCor(float hp)
    {
        float time = 0.7f;
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            _hpBarSlider.value = Mathf.Lerp(_hpBarSlider.value, hp, percent);

            yield return null;
        }

        _hpBarSlider.value = hp;

        _hpUpdateCoroutine = null;
    }
}
