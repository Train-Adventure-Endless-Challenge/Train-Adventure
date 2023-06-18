using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    VCamera _cam => VCamera.Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _cam.transform.rotation * Vector3.back,
            _cam.transform.rotation * Vector3.up);
    }
}
