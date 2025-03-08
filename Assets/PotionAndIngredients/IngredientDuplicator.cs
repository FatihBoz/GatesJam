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
    }
    /*
    private void OnMouseUp()
    {
        Debug.Log("Item deleted from Inventory");
    }
    */
    public void GetItem()
    {
        if (inventoryItem != null)
        {
            if (inventoryItem.quantity > 0)
            {
                inventoryItem = InventorySystem.instance.SearchItem(ingrName);

                currentIngredient = Instantiate(inventoryItem.ingredient.ingrPrefab, transform.position, Quaternion.identity);
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
}
