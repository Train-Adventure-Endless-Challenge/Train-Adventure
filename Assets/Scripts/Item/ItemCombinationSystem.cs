using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombinationSystem : MonoBehaviour
{

    /// <summary>
    /// 조합법 변수
    /// </summary>
    public Dictionary<HashSet<int>,int> _itemCombinationMethod = ItemDataManager.Instance.ItemCombinationMethod;
    private List<Item> _ingredientItem = new List<Item>();
    

    void Start()
    {

        ////////////////////////////////////// 테스트 ///////////////////////////////////////
        Combination();
    }

    void Update()
    {
        
    }

    void Combination()
    {
        List<int> ingredientId = new List<int>() { 1,2,3};
        foreach (Item item in _ingredientItem)
            ingredientId.Add(item.Id);


        foreach (HashSet<int> combination in _itemCombinationMethod.Keys)
        {
           HashSet<int> com = new HashSet<int>(combination);
           com.ExceptWith(ingredientId);

           if(com.Count <= 0) // 조합법 충족
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
