using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSystem : SceneSingleton<GearSystem>
{
    private int _gearAmount;
    public int GearAmount { get { return _gearAmount; } }

    public void AddGear(int addCount)
    {
        if (addCount < 0) return;

        _gearAmount += addCount;
        // TODO: 추후 UI 변경 등 함수 실행
    }

    public void SubGear(int subCount)
    {
        if (_gearAmount < subCount || subCount > 0) return;
        _gearAmount -= subCount;
        // TODO: 추후 UI 변경 등 함수 실행
    }


}
            