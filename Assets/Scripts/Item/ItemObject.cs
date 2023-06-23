using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int Id;

    [SerializeField] private ItemData _itemData;

    void Start()
    {
        GameObject itemObj =
            Instantiate(ItemDataManager.Instance.ItemPrefab[Id] as GameObject, transform.position, Quaternion.identity);
        itemObj.transform.parent = transform;

        _itemData = ItemDataManager.Instance.ItemData[Id];
    }

}
