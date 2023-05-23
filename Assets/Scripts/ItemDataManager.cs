using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemDataManager : GlobalSingleton<ItemDataManager>,IDataManager
{
    private Dictionary<int, ItemData> _itemData = new Dictionary<int, ItemData>();
    private Dictionary<int, Object> _itemPrefab = new Dictionary<int, Object>();
    private Dictionary<List<int>, int> _itemCombinationMethod = new Dictionary<List<int>, int>();

    public Dictionary<int, ItemData> ItemData { get { return _itemData; } }
    public Dictionary<int,Object> ItemPrefab { get { return _itemPrefab; } }
    public Dictionary<List<int>,int> ItemCombinationMethod { get { return _itemCombinationMethod; } }

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public void Load()
    {
        ItemData[] items = Resources.LoadAll<ItemData>("ScriptableObjects/Item");

        foreach (ItemData i in items)
            ItemData.Add(i.Id, i);

        Object[] prefabs = Resources.LoadAll<Object>("Prefabs/Item");

        foreach (Object pre in prefabs)
            ItemPrefab.Add(pre.GetComponent<Item>().Id, pre);

        // TODO: 조합법 json 파싱 후 집어넣기
        _itemCombinationMethod.Add(new List<int>() { 1, 2 }, 3); // 테스트

    }
}
