using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMachine : InteractionObject
{
    private int _necessaryGear;
    public override void Interact()
    {
        if(GearSystem.Instance.GearAmount >= _necessaryGear)
        {
            // TODO: 흔들림 초기화 함수 실행
            GearSystem.Instance.SubGear(_necessaryGear);
        }
    }
}
