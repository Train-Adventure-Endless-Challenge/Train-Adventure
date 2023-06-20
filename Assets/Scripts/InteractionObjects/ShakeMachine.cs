using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMachine : InteractionObject
{
    Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public override void Interact()
    {
        int necessaryGear = (int)ShakeManager.Instance.ShakeAmount * 5;
        if (GearSystem.Instance.GearAmount >= necessaryGear)
        {
            ShakeManager.Instance.ClearShake();
            GearSystem.Instance.SubGear(necessaryGear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Open", false);
        }
    }
}
