using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract void Hit(float damage, GameObject attacker);
    public abstract void Die();
}