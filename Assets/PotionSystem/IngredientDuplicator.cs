using UnityEngine;

public class IngredientDuplicator : MonoBehaviour
{
    public string ingrName;
    //[SerializeField] private GameObject InventoryItem;
    [SerializeField] private Camera mainCam;
    GameObject currentIngredient;
    private bool isDragging;

    private void OnMouseDown()
    {
        print("on mouse down");
        getItem();
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging && currentIngredient != null)
        {
            currentIngredient.transform.position = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient.transform.position = new Vector3(currentIngredient.transform.position.x, currentIngredient.transform.position.y, 0);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        dropItem();
        Debug.Log("Item deleted from Inventory");

    }

    public void getItem()
    {
        InventoryItem inventoryItem;
        inventoryItem = InventorySystem.instance.SearchItem(ingrName);
        if (inventoryItem != null)
        {
            if (inventoryItem.quantity > 0)
            {
                currentIngredient = Instantiate(inventoryItem.ingredient.ingrPrefab, transform.position, Quaternion.identity);
                InventorySystem.instance.DelItem(ingrName, 1);

                Debug.Log("Item Spawned and reduced quantity from Inventory. ");
            }
        }



    }

    public void dropItem()
    {
        //InventorySystem.instance.DelItem(ingrName.ToString(), 1);
    }
}
