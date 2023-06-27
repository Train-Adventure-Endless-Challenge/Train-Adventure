using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : SceneSingleton<GearManager>
{
    private int _gearAmount;

    /// <summary>
    /// 기어 수
    /// </summary>
    public int GearAmount { get { return _gearAmount; } }

    /// <summary>
    /// 기어 더하는 함수
    /// </summary>
    /// <param name="addCount">더할 기어 수</param>
    public void AddGear(int addCount)
    {
        if (addCount < 0) return;

        _gearAmount += addCount;
        IngameUIController.Instance.UpdateGear(_gearAmount);
    }

    /// <summary>
    /// 기어 빼는 함수
    /// </summary>
    /// <param name="subCount">뺄 기어 수</param>
    public void SubGear(int subCount)
    {
        if (_gearAmount < subCount || subCount < 0) return;
        _gearAmount -= subCount;

        IngameUIController.Instance.UpdateGear(_gearAmount);
    }


}
            