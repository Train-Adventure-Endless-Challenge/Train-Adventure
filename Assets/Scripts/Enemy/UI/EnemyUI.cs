using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private GameObject _exclamationMark;
    public Slider _hpBarSlider;

    private EnemyController _enemyController;
    private Coroutine _hpUpdateCoroutine;
    private bool _isHpUpdateCor;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    public void Init()
    {
        _hpBarSlider.maxValue = _enemyController.MaxHp;
        _hpBarSlider.value = _enemyController.MaxHp;
        Debug.Log(_enemyController.MaxHp);

        if(_enemyController.EnemyType != EnemyType.Boss)        // Boss는 HP UI 항상 표기
            _hpBarSlider.gameObject.SetActive(false);
    }

    public void UpdateHpUI(float hp)
    {
        if (_hpUpdateCoroutine != null || !_isHpUpdateCor)
        {
            StopAllCoroutines();
        }

        _hpUpdateCoroutine = StartCoroutine(UpdateHpCor(hp));
    }

    IEnumerator UpdateHpCor(float hp)
    {
        _isHpUpdateCor = true;

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

        _isHpUpdateCor = false;
        _hpUpdateCoroutine = null;
    }

    public void ExclamationMarkToggle(bool toggle)
    {
        _exclamationMark.SetActive(toggle);
    }

    public void DeactivateUI()
    {
        StopAllCoroutines();
        Destroy(_hpBarSlider.gameObject);
    }
}
