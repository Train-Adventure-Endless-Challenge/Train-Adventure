using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    public int AddGearAmount = 500;

    public KeyCode SkipKey = KeyCode.N;

    private void Start()
    {
        GearManager.Instance.AddGear(AddGearAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(SkipKey))
        {
            InGameManager.Instance.NextStage();
        }
    }
}