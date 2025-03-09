using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientDuplicator : MonoBehaviour
{
    public string ingrName;
    [SerializeField] private Camera mainCam;
    GameObject currentIngredient;
    private bool isDragging;

    public TextMeshProUGUI amount;

    InventoryItem inventoryItem;

    public GameObject ingredientPropPrefab;

    public ScriptableObject ScriptableObject;

    private List<TextMeshProUGUI> ingredientTextFields;

    private void Awake()
    {
        ingredientTextFields = new List<TextMeshProUGUI>(ingredientPropPrefab.GetComponentsInChildren<TextMeshProUGUI>());
        
        UpdateProps();
    }
    private void Start()
    {
        inventoryItem = InventorySystem.instance.SearchItem(ingrName);


    }

    private void OnMouseDown()
    {
        print("on mouse down");
        GetItem();  
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

    private void OnMouseEnter()
    {
        print("Fare üstüne geldi");
        ingredientPropPrefab.SetActive(true);

    }

    private void OnMouseExit()
    {
        print("Fare çýktý");
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
                currentIngredient.transform.parent = transform.parent;
                InventorySystem.instance.DelItem(ingrName, 1);
                UpdateText();

                Debug.Log("Item : " + inventoryItem.ingredient.ingrName + " Spawned and reduced quantity from Inventory. ");
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
            // ingredient'in tüm özelliklerini dinamik olarak güncelle
            Ingredients ingredient = inventoryItem.ingredient;

            // Bu sýralama, sýrasýyla Acidity, Density, Viscosity ve Temperature deðerlerinin TextMeshPro component'larýna atanmasýný saðlar.
            var properties = new List<float> { ingredient.Logically, ingredient.Healthy, ingredient.Sweetness, ingredient.Acidity };

            for (int i = 0; i < ingredientTextFields.Count && i < properties.Count; i++)
            {
                ingredientTextFields[i].text = properties[i].ToString("F1"); // Ýki ondalýklý olarak göster
            }
        }
    }
    

    

}
