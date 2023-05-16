using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSystem : MonoBehaviour
{
    private int _gearCount;
    public int GearCount { get { return _gearCount; } }

    public void AddGear(int addCount)
    {
        if (addCount < 0) return;

        _gearCount += addCount;
        // TODO: 추후 UI 변경 등 함수 실행
    }

    public void SubGear(int subCount)
    {
        if (_gearCount < subCount || subCount > 0) return;
        _gearCount -= subCount;
        // TODO: 추후 UI 변경 등 함수 실행
    }


}
