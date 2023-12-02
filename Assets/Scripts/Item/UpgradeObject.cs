using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeObject : InteractionObject
{

    [SerializeField] private GameObject _UICanvas;

    [SerializeField] private UnityEvent _interactEvent;
    public override void Interact()
    {
        _interactEvent.Invoke();
    }

}
