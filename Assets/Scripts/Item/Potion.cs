using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    [SerializeField] private float _healAmount = 100f;

    public override void EarnItem()
    {
        PlayerManager.Instance.gameObject.GetComponent<Player>().Heal(_healAmount);
        InventoryManager.Instance.DeleteItem(InventoryItem);
    }
}
