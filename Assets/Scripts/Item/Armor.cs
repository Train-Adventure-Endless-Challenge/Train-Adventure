using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    [SerializeField] protected Armortype _armorType;            // 방어구 종류

    public Armortype ArmorType { get { return _armorType; } }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
