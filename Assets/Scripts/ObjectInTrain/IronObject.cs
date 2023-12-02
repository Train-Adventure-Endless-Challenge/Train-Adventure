using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronObject : ObjectInTrain
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody _rigid;

    protected override void Start()
    {
        base.Start();
        _rigid = GetComponent<Rigidbody>();
    }
    public override void ActivateByShaking()
    {
        _rigid.isKinematic = false;
        _rigid.AddForce(100, 0, 0);
    }
}
