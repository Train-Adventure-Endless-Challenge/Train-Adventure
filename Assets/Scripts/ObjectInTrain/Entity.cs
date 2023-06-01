using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 피격과 죽음이 있는 모든 객체들의 상위 클래스
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// 피격 당했을 때, 체력이 닳았을 때 호출하는 함수
    /// </summary>
    /// <param name="damage">받은 데미지</param>
    /// <param name="attacker">떄린 객체</param>
    public abstract void Hit(float damage, GameObject attacker);
    /// <summary>
    /// 죽었거나 그와 같은 상태에 들어섰을 때 실행시키는 함수
    /// </summary>
    public abstract void Die();
}