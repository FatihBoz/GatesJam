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
        if (TableUI.TryGetComponent<TableUI>(out var tableUI))
        {
            tableUI.IngredientsToTable(collision.GetComponent<InventoryItem>().ingredient);
        }

        Destroy(collision.gameObject);
    }
}
