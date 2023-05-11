using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombinationSystem : MonoBehaviour
{

    /// <summary>
    /// 조합법 변수
    /// </summary>
    public static List<HashSet<int>> _itemCombinationMethod = new List<HashSet<int>>();                 
    private Item[] _ingredientItem = new Item[2];

    

    void Start()
    {
        _itemCombinationMethod.Add(new HashSet<int>() { 1,2});
        /*HashSet<int> a = new HashSet<int>(_itemCombinationMethod[0]);
        List<int> b = new List<int>() { 1, 2 };
        a.ExceptWith(b);
        
        Debug.Log(a.Count);
        Debug.Log(_itemCombinationMethod[0].Count);*/
    }

    void Update()
    {
        
    }

    void Combination()
    {
        List<int> ingredientId = new List<int>();
        ingredientId.Add(_ingredientItem[0].Id);
    }
}
