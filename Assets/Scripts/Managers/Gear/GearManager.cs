using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : SceneSingleton<GearManager>
{
    public List<GameObject> _gearTrasureBoxObjsList;

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

        IngameUIController.Instance.PopupText($"기어{addCount}개 획득");
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

    /// <summary>
    /// 다음 칸으로 넘어갈 시 전 칸의 기어 오브젝트 삭제
    /// </summary>
    public void DestroyGearObj()
    {
        if (_gearTrasureBoxObjsList.Count <= 0) return;

        foreach (GameObject gameObject in _gearTrasureBoxObjsList)
        {
            _gearTrasureBoxObjsList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
