using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject _floor;

    public virtual void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        
    }

    public void DestroyTrain()
    {
        Destroy(gameObject);
    }

}
