using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientDuplicator : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string ingrName;
    [SerializeField] private Camera mainCam;
    GameObject currentIngredient;
    private bool isDragging;

    public TextMeshProUGUI amount;

    InventoryItem inventoryItem;

    public GameObject ingredientPropPrefab;

    public ScriptableObject ScriptableObject;


    private void Awake()
    {        
        UpdateProps();
    }

    private void Start()
    {
        inventoryItem = InventorySystem.instance.SearchItem(ingrName);
    }

    private void Update()
    {
        if (isDragging && currentIngredient != null)
        {
            currentIngredient.transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient.transform.position = new Vector3(currentIngredient.transform.position.x, currentIngredient.transform.position.y, 0);
        }

        UpdateText();
        UpdateProps();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        GetItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ingredientPropPrefab.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ingredientPropPrefab.SetActive(false);
    }

    public void GetItem()
    {
        if (inventoryItem != null)
        {
            if (inventoryItem.quantity > 0)
            {
                inventoryItem = InventorySystem.instance.SearchItem(ingrName);

                currentIngredient = Instantiate(inventoryItem.ingredient.ingrPrefab, transform.position, Quaternion.identity);
                if (currentIngredient.TryGetComponent<Draggable>(out var drag))
                {
                    drag.isDragging = true;
                }

                //currentIngredient.transform.parent = transform.parent;
                InventorySystem.instance.DelItem(ingrName, 1);
                UpdateText();
            }
        }
    }



    public bool GetIsDragging()
    {
        return isDragging;
    }

    public void UpdateText()
    {
        if (inventoryItem != null)
        {
            amount.text = inventoryItem.quantity.ToString();
        }
            
    }
    
    public void UpdateProps()
    {
        if (ingredientPropPrefab != null && inventoryItem != null)
        {
            Ingredients ingredient = inventoryItem.ingredient;

            var properties = new List<float> { ingredient.Logically, ingredient.Healthy, ingredient.Sweetness, ingredient.Acidity };

            for (int i = 0; i < ingredientPropPrefab.transform.childCount && i < properties.Count; i++)
            {
                if (ingredientPropPrefab.transform.GetChild(i).TryGetComponent<RectTransform>(out var recttransform))
                {
                    recttransform.localScale = new Vector3(Mathf.Abs(1 * properties[i] * 1.2f), 1 * properties[i] * 1.2f, Mathf.Abs(1 * properties[i]) * 1.2f);
                }

            }           
        }
    }


}
