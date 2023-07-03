using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject _interactionUI;
    public abstract void Interact();

    public virtual void OnDetection()
    {
        _interactionUI.SetActive(true);
    }

    public virtual void OffDetection()
    {
        _interactionUI.SetActive(false);
    }
}
