using UnityEngine;

public class Cauldron : MonoBehaviour, ICauldron
{
    [SerializeField] private int maxScoopCountToFill = 3;

    private int currentScoopCount;

    public GameObject TableUI;

    private void Start()
    {
        currentScoopCount = maxScoopCountToFill;
    }

    public bool DecreaseScoopCount()
    {
        if(currentScoopCount > 0)
        {
            currentScoopCount--;
            return true;
        }
        return false;
    }

    public Color GetPotionColor()
    {
        return Color.magenta;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TableUI.GetComponent<TableUI>().IngredientsToTable(collision.GetComponent<InventoryItem>().ingredient);

        print("The item affected cauldron : " + collision.GetComponent<InventoryItem>().ingredient.ingrName);

        Destroy(collision.gameObject);
    }
}
