using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private Material[] _sky;
    // 0 : 낮
    // 1 : 밤

    private float _degree;

    void Start()
    {
        StartCoroutine(ChangeSkyboxCor(0));

        _degree = 0;
    }

    void Update()
    {
        _degree += Time.deltaTime * 1.5f;
        if (_degree >= 360)
            _degree = 0;

        RenderSettings.skybox.SetFloat("_Rotation", _degree);
    }

    public IEnumerator ChangeSkyboxCor(int index)
    {
        yield return StartCoroutine(FadeSkyboxCor(1, 0));

        yield return RenderSettings.skybox = _sky[index];

        yield return StartCoroutine(FadeSkyboxCor(0, 1));
    }

    private IEnumerator FadeSkyboxCor(float start, float end)
    {
        float fadeTime = 1f;
        float currentTiem = 0f;
        float percent = 0f;

        while (percent < 1)
        {
            currentTiem += Time.deltaTime;
            percent = currentTiem / fadeTime;

            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(start, end, percent));

            yield return null;
        }

        RenderSettings.skybox.SetFloat("_Exposure", end);
    }
}

