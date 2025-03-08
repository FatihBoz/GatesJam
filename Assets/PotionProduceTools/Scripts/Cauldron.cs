using Unity.VisualScripting;
using UnityEngine;

public class Cauldron : MonoBehaviour, ICauldron
{
    [SerializeField] private int maxScoopCountToFill = 3;

    private int currentScoopCount;

    public GameObject TableUI;

    private TriColor triColor;

    private void Start()
    {
        currentScoopCount = maxScoopCountToFill;
        triColor = new TriColor(0, 0, 0);
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
        Color32 tempColor = triColor.GetAverageColor();
        print("cauldron color:" + tempColor.ToString());
        triColor.ResetColor();
        return tempColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TableUI.TryGetComponent<TableUI>(out var tableUI) && collision.TryGetComponent<InventoryItem>(out var item))
        {
            triColor.AddColor(item.ingredient.color);
            float percentage = 1f;
            Sliceable sliceable = tableUI.GetComponent<Sliceable>();
            if (sliceable!=null)
            {
                percentage = sliceable.percentage;
            }
            tableUI.IngredientsToTable(item.ingredient,percentage);
            Destroy(collision.gameObject);
        }

        
    }
}
