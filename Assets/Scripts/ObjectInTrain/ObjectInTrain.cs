using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInTrain : Entity
{
    private int _activateShakingCondition;
    public override void Die()
    {
    }

    public override void Hit(float damage, GameObject attacker)
    {
    }

    public abstract void ActivateByShaking();

    public void ShakingCheck()
    {
        //if(흔들림 체크)
        ActivateByShaking();
    }


    protected virtual void Update()
    {
        ActivateByShaking();
    }
    
}
