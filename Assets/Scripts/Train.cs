using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject _floor;

    public virtual void Init()
    {
        
    }

    public void DestroyGameObejct()
    {
        Destroy(gameObject);
    }

}
