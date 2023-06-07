using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMachine : InteractionObject
{
    public override void Interact()
    {
        int necessaryGear = (int)ShakeManager.Instance.ShakeAmount * 5;
        if (GearSystem.Instance.GearAmount >= necessaryGear)
        {
            ShakeManager.Instance.ClearShake();
            GearSystem.Instance.SubGear(necessaryGear);
        }
    }
}
