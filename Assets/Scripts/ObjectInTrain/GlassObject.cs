using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassObject : ObjectInTrain
{
    [SerializeField] private GameObject _glassFragmentPrefab;

    public override void ActivateByShaking()
    {
        Die();
    }

    [ContextMenu("OnDie")]
    public override void Die()
    {
        Instantiate(_glassFragmentPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
