using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCombinationSystem : MonoBehaviour
{

    /// <summary>
    /// 조합법 변수
    /// </summary>
    public Dictionary<List<int>, int> _itemCombinationMethod;
    private List<Item> _ingredientItem = new List<Item>();


    void Start()
    {
        _itemCombinationMethod = ItemDataManager.Instance.ItemCombinationMethod;
        ////////////////////////////////////// 테스트 ///////////////////////////////////////
        Combination();
    }

    void Update()
    {

    }

    void Combination()
    {
        List<int> ingredientId = new List<int>();
        foreach (Item item in _ingredientItem)
            ingredientId.Add(item.Id);


        foreach (List<int> combination in _itemCombinationMethod.Keys)
        {
            List<int> com = new List<int>(combination);
            com = com.Except(ingredientId).ToList();

            if (com.Count <= 0) // 조합법 충족
            {
                Item combinationItem = new Item();
                // TODO: itemData적용, itemCombinationMethod의 키값-> 조합아이템 id
                // TODO: UI상에 띄우기 -> 인벤토리에 옮기면 그곳에 옮기고 그냥 끄면 맨뒤에 추가
                return;
            }
            else
            {
                Item combinationItem = new Item();
                // TODO: 랜덤한 아이템 데이터 적용
                // TODO: UI상에 띄우기 -> 인벤토리에 옮기면 그곳에 옮기고 그냥 끄면 맨뒤에 추가
                return;
            }
        }

    }
}
