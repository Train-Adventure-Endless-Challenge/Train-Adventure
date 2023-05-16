using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Transform[] _backgrounds;
    [SerializeField] private float _backgroundWidth;
    [SerializeField] private float _speed;

    private Vector3 _firstBackgroundPosition;
    private int _backgroundIndex;

    private void Start()
    {
        _firstBackgroundPosition = _backgrounds[0].transform.localPosition;

        _backgroundIndex = 0;
    }

    private void Update()
    {
        if (_backgrounds[_backgroundIndex].localPosition.z <= _firstBackgroundPosition.z)
        {
            Vector3 nextPosition = _backgrounds[_backgroundIndex].localPosition;
            nextPosition = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z + _backgroundWidth * _backgrounds.Length);
            _backgrounds[_backgroundIndex].localPosition = nextPosition;
            _backgroundIndex++;
            _backgroundIndex %= _backgrounds.Length;
        }

        // 배경 이동
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].Translate(-Vector3.forward * _speed * Time.deltaTime);
        }
    }
}
