using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        _hpBarSlider.value = _enemyController.HP;
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
        while (true)
        {
            yield return null;

            _hpBarSlider.value = Mathf.Lerp(_hpBarSlider.value, hp, 10 * Time.deltaTime);
            
            if (Mathf.Abs(_hpBarSlider.value - hp) <= 0.1f)
            {
                _hpUpdateCoroutine = null;
                break;
            }
        }
        _hpBarSlider.value = hp;
    }
}
