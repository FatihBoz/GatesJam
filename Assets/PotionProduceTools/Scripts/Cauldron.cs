using UnityEngine;

public class Cauldron : MonoBehaviour, ICauldron
{
    [SerializeField] private int maxScoopCountToFill = 3;

    private int currentScoopCount = 1;

    public GameObject TableUI;

    private TriColor triColor;

    public int ScoopCount => currentScoopCount;

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
        triColor.ResetColor();
        return tempColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TableUI.TryGetComponent<TableUI>(out var tableUI) && collision.TryGetComponent<InventoryItem>(out var item))
        {
            triColor.AddColor(item.ingredient.color);
            float percentage = 1f;
            Sliceable sliceable = item.GetComponent<Sliceable>();
            if (sliceable!=null)
            {
                percentage = sliceable.percentage;
            }


            tableUI.IngredientsToTable(item.ingredient,percentage);
            Destroy(collision.gameObject);
        }
    }

    private void OnEnable()
    {
        Scoop.OnCauldronIsEmpty += OnCauldronIsEmpty;
    }

    private void OnCauldronIsEmpty()
    {
        triColor.ResetColor();
    }

    private void OnDisable()
    {
        Scoop.OnCauldronIsEmpty -= OnCauldronIsEmpty;
    }
}
