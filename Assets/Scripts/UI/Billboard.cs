using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    CameraManager _cam => CameraManager.Instance;

    void Update()
    {
        transform.LookAt(transform.position + _cam.transform.rotation * Vector3.back,
            _cam.transform.rotation * Vector3.up);
    }
}
