using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnMouseCheckItem : MonoBehaviour
{
    private Canvas _inventoryCanvas;
    private GraphicRaycaster _graphicRaycaster;
    private PointerEventData _pointerEventData;

    [SerializeField] GameObject _descriptionImagePrefab;
    [SerializeField] private bool _isShow;

    private void Awake()
    {
        _inventoryCanvas = GetComponent<Canvas>();
        _graphicRaycaster = _inventoryCanvas.GetComponent<GraphicRaycaster>();

        _pointerEventData = new PointerEventData(null);
        
    }

    void Update()
    {
        OnMouseCheck();
    }

    private void OnMouseCheck()
    {
        _pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(_pointerEventData, results);

        if (results.Count > 0 && !_isShow)
        {
            if (results[0].gameObject.TryGetComponent<InventoryItem>(out InventoryItem item))
            {
                StartCoroutine(ShowDescriptionImageCoroutine(item));
            }
        }
    }

    private IEnumerator ShowDescriptionImageCoroutine(InventoryItem inventoryItem)
    {
        _isShow = true;
        GameObject descriptionObj = null;
        while (true)
        {
            //설명 이미지 생성
            if (descriptionObj == null)
            {
                descriptionObj = Instantiate(_descriptionImagePrefab, inventoryItem.transform.position + new Vector3(200f, -200f, 0), Quaternion.identity);
                descriptionObj.transform.SetParent(_inventoryCanvas.transform);
                descriptionObj.GetComponent<RectTransform>().localScale = Vector3.one;

                Item item = inventoryItem._item;
                descriptionObj.GetComponent<DescriptionImage>().Init(item.Name, item.Description, $"{item.Durability} / {item.ItemData.MaxDurability}");
            }

            #region Check OnMouse

            _pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(_pointerEventData, results);

            if (!results[0].gameObject.transform.GetComponent<InventoryItem>())
            {
                _isShow = false;
                Destroy(descriptionObj);
                yield break;
            }

            #endregion
            yield return new WaitForEndOfFrame();
        }
    }

}
