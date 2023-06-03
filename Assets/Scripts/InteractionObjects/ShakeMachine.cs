using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMachine : InteractionObject
{
    /// <summary>
    /// 필요한 기어 수
    /// </summary>
    private int _necessaryGear;
    public override void Interact()
    {
        if(GearSystem.Instance.GearAmount >= _necessaryGear)
        {
            ShakeManager.Instance.ClearShake();
            GearSystem.Instance.SubGear(_necessaryGear);
        }
    }
}
