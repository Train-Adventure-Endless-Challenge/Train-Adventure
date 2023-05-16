using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [SerializeField] private Material[] sky;

    private float degree;

    void Start()
    {
        StartCoroutine(ChangeSkyboxCor(0));

        degree = 0;
    }

    void Update()
    {
        degree += Time.deltaTime * 1.5f;
        if (degree >= 360)
            degree = 0;

        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }

    public IEnumerator ChangeSkyboxCor(int index)
    {
        yield return StartCoroutine(FadeSkyboxCor(1, 0));

        yield return RenderSettings.skybox = sky[index];

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

