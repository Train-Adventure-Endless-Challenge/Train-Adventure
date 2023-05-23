using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCombinationSystem : MonoBehaviour
{

    /// <summary>
    /// 조합법 변수
    /// </summary>
    public Dictionary<List<int>, int> _itemCombinationMethods;
    private List<Item> _ingredientItems = new List<Item>();

    public InventorySlot[] ingredientSlot;
    public InventorySlot resultSlot;


    void Start()
    {
        _itemCombinationMethods = ItemDataManager.Instance.ItemCombinationMethod;

        ////////////////////////////////////// 테스트 ///////////////////////////////////////
        //Combination();
    }

    void Combination()
    {
        List<int> ingredientIds = new List<int>();

        foreach(InventorySlot slot in ingredientSlot)
            ingredientIds.Add(slot.GetComponentInChildren<InventoryItem>()._item.Id);


        foreach (List<int> combination in _itemCombinationMethods.Keys)
        {
            List<int> com = new List<int>(combination);
            com = com.Except(ingredientIds).ToList();


            if (com.Count <= 0) // 조합법 충족
            {
                Item combinationItem = new Item(ItemDataManager.Instance.ItemData[_itemCombinationMethods[com]]);
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
